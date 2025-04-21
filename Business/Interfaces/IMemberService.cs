using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<bool> CreateMemberAsync(MemberRegistrationForm form);
    Task<IEnumerable<Member>> GetAllMembers();
    Task<Member?> GetMemberAsync(Expression<Func<MemberEntity, bool>> expression);
    Task<bool> UpdateAsync(MemberUpdateForm updatedMember);
    Task<bool> DeleteAsync(string id);
    Task<string> GetUserFullName(string name);
    Task<string> GetUserImage(string name);
    Task<bool> ExistsAsync(string email);
}