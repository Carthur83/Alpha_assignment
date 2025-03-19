using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class UserService(UserManager<AppUser> userManager) : IUserService
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<bool> CreateUserAsync(CreateAccountForm form)
    {
        if (form != null)
        {
            var appUser = UserFactory.Create(form);

            var result = await _userManager.CreateAsync(appUser, form.Password);
            return result.Succeeded;
        }

        return false;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        if (await _userManager.Users.AnyAsync(u => u.Email == email))
        {
            return true;
        }

        return false;
    }
}
