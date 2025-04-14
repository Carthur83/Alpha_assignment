using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Business.Services;

public class MemberService(UserManager<MemberEntity> userManager) : IMemberService
{
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> CreateMemberAsync(MemberRegistrationForm form)
    {
        if (form != null)
        {
            if (form.Password == null)
            {
                form.Password = "BytMig123!";
            }



            var memberEntity = MemberFactory.CreateEntity(form);

            if (form.Day > 0 && form.Month > 0 && form.Year > 0)
            {
                memberEntity.DateOfBirth = new DateOnly(form.Year, form.Month, form.Day);
            }

            var result = await _userManager.CreateAsync(memberEntity, form.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(memberEntity, "User");
            }
            return result.Succeeded;
        }

        return false;
    }

    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        var entities = await _userManager.Users
            .Include(member => member.Address)
            .ToListAsync();
        var members = entities.Select(MemberFactory.CreateModel);
        return members;
    }

    public async Task<Member?> GetMemberAsync(Expression<Func<MemberEntity, bool>> expression)
    {
        if (expression == null)
            return null;

        var existingEntity = await _userManager.Users
            .Include(member => member.Address)
            .FirstOrDefaultAsync(expression);

        if (existingEntity == null)
            return null;

        var member = MemberFactory.CreateModel(existingEntity);

        if (existingEntity.DateOfBirth.HasValue)
        {
            var dob = existingEntity.DateOfBirth.Value;
            member.Day = dob.Day;
            member.Month = dob.Month;
            member.Year = dob.Year;
        }

        return member;
    }

    public async Task<bool> UpdateAsync(MemberUpdateForm updatedMember)
    {
        if (updatedMember == null)
            return false;

        var existingEntity = await _userManager.Users
            .Include(member => member.Address)
            .FirstOrDefaultAsync(x => x.Id == updatedMember.Id);

        if (existingEntity == null)
            return false;

        MemberFactory.CreateEntity(updatedMember, existingEntity);

        var result = await _userManager.UpdateAsync(existingEntity);
        return result.Succeeded;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (id == null)
            return false;

        var entity = await _userManager.FindByIdAsync(id);

        if (entity == null)
            return false;

        var result = await _userManager.DeleteAsync(entity);

        return result.Succeeded;
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
