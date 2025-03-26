using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProjectsController(UserManager<MemberEntity> userManager, IProjectService projectService) : Controller
    {
        private readonly UserManager<MemberEntity> _userManager = userManager;
        private readonly IProjectService _projectService = projectService;

        
        public async Task<IActionResult> Projects(string status = "all")
        {
            var projects = await _projectService.GetAllProjectsAsync();

            var viewModel = new ProjectsViewModel
            {
                Projects = projects,
                TotalCount = projects.Count(),
                StartedCount = projects.Count(x => x.Status == "started"),
                CompletedCount = projects.Count(x => x.Status == "completed")
            };

            if (status == "started")
            {
                viewModel.Projects = viewModel.Projects.Where(x => x.Status == status);               
            }
            else if (status == "completed")
            {
                viewModel.Projects = viewModel.Projects.Where(x => x.Status == status);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["UserName"] = user.FirstName + " " + user.LastName;
            }
       
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(AddProjectForm form)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)
                        .ToList()
                    );

                return BadRequest(new { success = false, errors });
            }

            var addProjectDto = new AddProject
            {
                ProjectName = form.ProjectName,
                ClientName = form.ClientName,
                Description = form.Description,
                StartDate = form.StartDate,
                EndDate = form.EndDate,
                Budget = form.Budget
            };

            await _projectService.AddProject(addProjectDto);

            return Ok();
        }
    }
}
