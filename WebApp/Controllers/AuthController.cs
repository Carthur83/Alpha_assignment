using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult CreateAccount()
        {
            var formData = new CreateAccountFormModel();
            return View(formData);
        }

        [HttpPost]
        public IActionResult CreateAccount(CreateAccountFormModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string test)
        {
            return RedirectToAction("Projects", "Projects");
        }
    }
}
