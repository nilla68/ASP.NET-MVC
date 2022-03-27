using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omnivus.Data;
using Omnivus.Helpers;
using Omnivus.Models;

namespace Omnivus.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AddressManager _addressManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AddressManager addressManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addressManager = addressManager;
        }

        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterViewModel();

            if (returnUrl != null)
                model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                }

                if (!_roleManager.Roles.Any(r => r.Name == "user"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                if (!_userManager.Users.Any())
                    registerViewModel.RoleName = "admin";

                var user = new ApplicationUser()
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var address = new ApplicationAddress
                    {
                        Street = registerViewModel.Street,
                        PostCode = registerViewModel.PostCode,
                        City = registerViewModel.City
                    };

                    await _addressManager.AddOrUpdateToAddressAsync(user, address);
                    await _userManager.AddToRoleAsync(user, registerViewModel.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (registerViewModel.ReturnUrl == null || registerViewModel.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(registerViewModel.ReturnUrl);
                }
                else
                {
                    registerViewModel.Error = result.Errors.Last().Description;
                }
            }

            return View(registerViewModel);
        }

        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");

            var model = new LoginViewModel();
            if (returnUrl != null)
                model.ReturnUrl = returnUrl;

            ViewData["Error"] = "";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, isPersistent: false, false);
                if (result.Succeeded)
                {
                    if (loginViewModel.ReturnUrl == null || loginViewModel.ReturnUrl == "/")
                        return RedirectToAction("Index", "Home");
                    else
                        return LocalRedirect(loginViewModel.ReturnUrl);
                }
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
