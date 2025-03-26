using Business.Models;

namespace Business.Interfaces;

public interface IMemberService
{
    Task<bool> CreateMemberAsync(MemberRegistrationForm form);
    Task<bool> ExistsAsync(string email);
}