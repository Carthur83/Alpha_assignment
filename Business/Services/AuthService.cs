using Business.Dtos;
using Business.Hubs;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Business.Services;

public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager, INotificationService notificationService) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly INotificationService _notificationService = notificationService;

    public async Task<SignInResult> LogInAsync(LogInForm form)
    {
        var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(form.Email);
            if (user != null)
            {
                var notificationEntity = new NotificationEntity
                {
                    Message = $"{user.FirstName} {user.LastName} signed in",
                    NotificationTypeId = 1,
                    Image = user.ImageFile!                  
                };

                await _notificationService.AddNotificationAsync(notificationEntity, user.Id);
                
            }
        }

        return result;
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
