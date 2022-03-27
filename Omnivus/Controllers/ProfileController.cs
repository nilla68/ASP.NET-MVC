using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omnivus.Data;
using Omnivus.Helpers;
using Omnivus.Models;

namespace Omnivus.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AddressManager _addressManager;
        private readonly IWebHostEnvironment _host;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, AddressManager addressManager, IWebHostEnvironment host)
        {
            _context = context;
            _userManager = userManager;
            _addressManager = addressManager;
            _host = host;
        }

        public async Task<IActionResult> Index(string returnUrl = null)
        {
            var userAddresses = await _context.UserAddresses
                .Include(a => a.Address)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.UserId == User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);

            ProfileViewModel model;
            if (userAddresses is null)
            {
                model = new ProfileViewModel();
            }
            else
            {
                model = new ProfileViewModel
                {
                    Street = userAddresses.Address.Street,
                    PostCode = userAddresses.Address.PostCode,
                    City = userAddresses.Address.City,
                    ProfileImageUrl = userAddresses?.User?.ProfileImage
                };
            }

            model.FirstName = userAddresses.User.FirstName;
            model.LastName = userAddresses.User.LastName;

            var userRole = (await _userManager.GetRolesAsync(userAddresses.User)).FirstOrDefault();
            model.Role = userRole;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (model.File is not null)
                {
                    string wwwrootPath = _host.WebRootPath;
                    string imageDirectoryPath = $"{wwwrootPath}/img/users";
                    string fileName = $"{Path.GetFileNameWithoutExtension(model.File.FileName)}_{Guid.NewGuid()}{Path.GetExtension(model.File.FileName)}";
                    string imageurl = $"/img/users/{fileName}";
                    string filePath = Path.Combine(imageDirectoryPath, fileName);

                    if (user?.ProfileImage is not null)
                    {
                        string oldProfileImagePath = Path.Combine(wwwrootPath, user?.ProfileImage);

                        if (System.IO.File.Exists(oldProfileImagePath))
                            System.IO.File.Delete(oldProfileImagePath);
                    }

                    if (!Directory.Exists(imageDirectoryPath))
                        Directory.CreateDirectory(imageDirectoryPath);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fs);
                    }

                    user.ProfileImage = imageurl;
                    model.ProfileImageUrl = imageurl;
                }
                else
                {
                    model.ProfileImageUrl = user.ProfileImage;
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await _userManager.UpdateAsync(user);

                var address = new ApplicationAddress
                {
                    Street = model.Street,
                    PostCode = model.PostCode,
                    City = model.City
                };

                await _addressManager.AddOrUpdateToAddressAsync(user, address);

                model.Role = model.Role;

                return View(model);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
