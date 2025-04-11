using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController(IMemberService memberService, SignInManager<MemberEntity> signInManager) : Controller
{
    private readonly IMemberService _memberService = memberService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;

    public IActionResult CreateAccount()
    {
        var formData = new MemberSignUpForm();
        return View(formData);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(MemberSignUpForm formData)
    {
        if (!ModelState.IsValid)
        {
            return View(formData);
        }

        if (await _memberService.ExistsAsync(formData.Email))
        {
            ModelState.AddModelError("Exists", "User Already Exists");
            return View(formData);
        }

        var memberRegistrationForm = new MemberRegistrationForm
        {
            FirstName = formData.FirstName,
            LastName = formData.LastName,
            Email = formData.Email,
            Password = formData.Password
        };

        var result = await _memberService.CreateMemberAsync(memberRegistrationForm);
        if (result)
            return RedirectToAction("LogIn", "Auth");

        ModelState.AddModelError("NotCreated", "User not created");
        return View(formData);
    }

    public IActionResult Login()
    {
        return RedirectToAction("Projects", "Projects", new { status = "all" });
    }

    [HttpPost]
    public async Task<IActionResult> Login(MemberLogInForm form)
    {
        if (ModelState.IsValid)
         {
            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Projects", "Projects", new { status = "all" });
            }
        }

        ViewBag.ErrorMessage = "Incorrect email or password";
        return View(form);
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }

    public IActionResult AdminLogIn()
    {
        return View();
    }
}


