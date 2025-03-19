using Business.Models;

namespace Business.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateAccountForm form);
    Task<bool> ExistsAsync(string email);
}