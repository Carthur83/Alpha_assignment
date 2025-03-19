using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ProjectsController(UserManager<AppUser> userManager) : Controller
    {
        private readonly UserManager<AppUser> _userManager = userManager;

        [Route("projects")]
        public async Task<IActionResult> Projects()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["UserName"] = user.FirstName + " " + user.LastName;
            }
            return View();
        }
    }
}
