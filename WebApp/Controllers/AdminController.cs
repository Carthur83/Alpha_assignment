using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(IMemberService memberService, IWebHostEnvironment environment) : Controller
{
    private readonly IMemberService _memberService = memberService;
    private readonly IWebHostEnvironment _environment = environment;

    public async Task<IActionResult> Members()
    {
        var viewModel = new MembersViewModel
        {
            Members = await _memberService.GetAllMembers()
        };

        return View(viewModel);
    }

    public IActionResult AddMember()
    {
        var viewModel = new MembersViewModel();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(AddMemberForm form)
    {
        string newImageName = null!;

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

        if (form.ImageFile != null)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "memberimages");
            Directory.CreateDirectory(uploadFolder);

            newImageName = $"{Guid.NewGuid()}_{Path.GetFileName(form.ImageFile.FileName)}";
            var filePath = Path.Combine(uploadFolder, newImageName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await form.ImageFile.CopyToAsync(stream);
        }


        var memberRegistrationForm = new MemberRegistrationForm
        {
            ImageFile = newImageName,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            JobTitle = form.JobTitle,
            Phone = form.Phone,
            Street = form.Street,
            PostalCode = form.PostalCode,
            City = form.City,
            Day = form.Day,
            Month = form.Month,
            Year = form.Year
        };

        var result = await _memberService.CreateMemberAsync(memberRegistrationForm);

        return Ok();
    }

    [HttpGet]
    [Route("admin/edit/{id}")]
    public async Task<IActionResult> EditMember(string id)
    {

        var member = await _memberService.GetMemberAsync(x => x.Id == id);

        if (member == null)
            return NotFound();

        var form = new EditMemberForm
        {
            Id = member.Id,
            ImageFileName = member.ImageFileName,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phone = member.Phone,
            JobTitle = member.JobTitle,
            Street = member.Street,
            City = member.City,
            PostalCode = member.PostalCode,
            Day = member.Day,
            Month = member.Month,
            Year = member.Year
        };

        return Json(form);
    }

    [HttpPost]
    public async Task<IActionResult> EditMember(EditMemberForm form)
    {
        string newImageName = null!;

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

        if (form.ImageFile != null)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "memberimages");
            Directory.CreateDirectory(uploadFolder);

            if (!string.IsNullOrEmpty(form.ImageFileName))
            {
                var existingFilePath = Path.Combine(uploadFolder, form.ImageFileName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            newImageName = $"{Guid.NewGuid()}_{Path.GetFileName(form.ImageFile.FileName)}";
            var filePath = Path.Combine(uploadFolder, newImageName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await form.ImageFile.CopyToAsync(stream);
        }

        var memberUpdateForm = new MemberUpdateForm
        {
            Id = form.Id,
            ImageFileName = newImageName,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            JobTitle = form.JobTitle,
            Phone = form.Phone,
            Street = form.Street,
            PostalCode = form.PostalCode,
            City = form.City,
            Day = form.Day,
            Month = form.Month,
            Year = form.Year
        };

        await _memberService.UpdateAsync(memberUpdateForm);

        return Ok();
    }

    [HttpPost]
    [Route("admin/delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var member = await _memberService.GetMemberAsync(x => x.Id == id);

        var result = await _memberService.DeleteAsync(id);
        if (result)
        {
            var uploadFolder = Path.Combine(_environment.WebRootPath, "memberimages");
            Directory.CreateDirectory(uploadFolder);

            if (!string.IsNullOrEmpty(member?.ImageFileName))
            {
                var existingFilePath = Path.Combine(uploadFolder, member.ImageFileName);
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            return Ok();
        }

        return NotFound();
    }

    [AllowAnonymous]
    [Route("denied")]
    public IActionResult Denied()
    {
        return View();
    }
}
