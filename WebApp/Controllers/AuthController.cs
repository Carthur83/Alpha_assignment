using Business.Interfaces;
using Business.Models;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AuthController(IUserService userservice, SignInManager<AppUser> signInManager) : Controller
    {
        private readonly IUserService _userService = userservice;
        private readonly SignInManager<AppUser> _signInManager = signInManager;

        public IActionResult CreateAccount()
        {
            var formData = new CreateAccountForm();
            return View(formData);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountForm formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            if (await _userService.ExistsAsync(formData.Email))
            {
                ModelState.AddModelError("Exists", "User Already Exists");
                return View(formData);
            }

            var result = await _userService.CreateUserAsync(formData);
            if (result)
                return RedirectToAction("LogIn", "Auth");

            ModelState.AddModelError("NotCreated", "User not created");
            return View(formData);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogInForm formData)
        {
            if (ModelState.IsValid)
             {
                var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Projects", "Projects");
                }
            }

            ViewBag.ErrorMessage = "Incorrect email or password";
            return View(formData);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
