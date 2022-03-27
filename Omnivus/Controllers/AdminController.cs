using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Omnivus.Data;
using Omnivus.Helpers;
using Omnivus.Models;

namespace Omnivus.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AddressManager _addressManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AddressManager addressManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _addressManager = addressManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return View("Error");

            var address = await _addressManager.GetAddressAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);
            var userRole = rolesForUser.FirstOrDefault();
            var roles = _roleManager.Roles.Select(r => new SelectListItem(r.Name, r.Name)).ToList();

            foreach (var role in roles)
            {
                if (role.Value == userRole)
                    role.Selected = true;
            }

            var userViewModel = new EditUserViewModel
            {
                Id = id,
                Role = userRole,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Street = address.Street,
                PostCode = address.PostCode,
                City = address.City,
                Roles = roles
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);

                if (user is null)
                    throw new Exception("User not found");

                var rolesForUser = await _userManager.GetRolesAsync(user);
                var userRole = rolesForUser.FirstOrDefault();

                if (userRole is not null)
                    await _userManager.RemoveFromRoleAsync(user, userRole);

                await _userManager.AddToRoleAsync(user, userViewModel.Role);

                var address = new ApplicationAddress
                {
                    Street = userViewModel.Street,
                    PostCode = userViewModel.PostCode,
                    City = userViewModel.City
                };

                await _addressManager.AddOrUpdateToAddressAsync(user, address);

                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Users", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> Roles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);
            var editRoleViewModel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == editRoleViewModel.Id);
                var result = await _roleManager.SetRoleNameAsync(role, editRoleViewModel.RoleName);
                var updateResult = await _roleManager.UpdateAsync(role);

                return RedirectToAction("Roles", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);
            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Roles", "Admin");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel addRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole(addRoleViewModel.RoleName));

                return RedirectToAction("Roles", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
