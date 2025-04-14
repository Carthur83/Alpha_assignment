using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;
using Business.Dtos;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.Controllers;

[Authorize]
public class ProjectsController(UserManager<MemberEntity> userManager, IProjectService projectService, IClientService clientService, IStatusService statusService) : Controller
{
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly IProjectService _projectService = projectService;
    private readonly IClientService _clientService = clientService;
    private readonly IStatusService _statusService = statusService;

    [Route("projects/{status?}")]
    public async Task<IActionResult> Projects(string status = "all")
    {
        var projects = await _projectService.GetAllProjectsAsync();

        var viewModel = new ProjectsViewModel
        {
            Projects = projects.Reverse(),
            TotalCount = projects.Count(),
            StartedCount = projects.Count(x => x.Status == "started"),
            CompletedCount = projects.Count(x => x.Status == "completed")
        };

        if (status == "started")
        {
            viewModel.Projects = viewModel.Projects.Where(x => x.Status == status).Reverse();
        }
        else if (status == "completed")
        {
            viewModel.Projects = viewModel.Projects.Where(x => x.Status == status).Reverse();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            ViewData["UserName"] = user.FirstName + " " + user.LastName;
        }

        return View(viewModel);
    }

    [HttpPost]
    [Route("projects/add")]
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
            Description = form.RichTextContent,
            StartDate = form.StartDate ?? DateTime.Today,
            EndDate = form.EndDate ?? DateTime.Today,
            Budget = form.Budget
        };

        await _projectService.AddProject(addProjectDto);

        return Ok();
    }

    [HttpPost]
    [Route("projects/edit")]
    public async Task<IActionResult> EditProject(EditProjectForm form)
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

        var updateForm = new ProjectUpdateForm
        {
            Id = form.Id,
            ProjectName = form.ProjectName,
            ClientId = form.ClientId,
            Description = form.EditRichTextContent!,
            StartDate = form.StartDate ?? DateTime.Today,
            EndDate = form.EndDate ?? DateTime.Today,
            Budget = form.Budget,
            StatusId = form.StatusId
        };

        await _projectService.UpdateAsync(updateForm);

        return Ok();
    }

    // tagit hjälp av chatgpt
    [HttpGet]
    [Route("projects/edit/{id}")]
    public async Task<IActionResult> EditProject(int id)
    {
        var project = await _projectService.GetProjectAsync(x => x.Id == id);

        if (project == null)
            return NotFound();

        var clients = await _clientService.GetAllClientsAsync();
        var statuses = await _statusService.GetAllAsync();
        var form = new EditProjectForm
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            ClientId = project.ClientId,
            EditRichTextContent = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget,
            StatusId = project.StatusId,
            Clients = clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ClientName
            }).ToList(),
            Statuses = statuses.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Status
            }).ToList()
        };

        return Json(form);
    }

    [HttpPost]
    [Route("projects/delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _projectService.DeleteAsync(x => x.Id == id);

        return RedirectToAction("projects/all");
    }
}
