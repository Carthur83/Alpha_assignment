using Business.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<SignInResult> LogInAsync(LogInForm form);
    Task LogOutAsync();
}