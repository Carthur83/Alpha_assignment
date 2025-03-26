using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> CreateMemberAsync(MemberRegistrationForm form)
    {
        if (form != null)
        {
            var memberEntity = MemberFactory.CreateEntity(form);

            var result = await _userManager.CreateAsync(memberEntity, form.Password);
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
