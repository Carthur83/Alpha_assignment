using Business.Hubs;
using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController(INotificationService notificationService, IHubContext<NotificationHub> notificationHub) : ControllerBase
{
    private readonly INotificationService _notificationService = notificationService;
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;

    [HttpPost]
    public async Task<IActionResult> CreateNotification(NotificationEntity notification)
    {
        await _notificationService.AddNotificationAsync(notification, "anonymous");
        return Ok(new { success = true });
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications()
    {
        var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        if (string.IsNullOrEmpty(memberId))
            return Unauthorized();

        var notifications = await _notificationService.GetNotificationsAsync(memberId);
        return Ok(notifications);
    }

    [HttpPost("dismiss/{id}")]
    public async Task<IActionResult> DismissNotification(string id)
    {
        var memberId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        if (string.IsNullOrEmpty(memberId))
            return Unauthorized();

        await _notificationService.DismissNotificationAsync(id, memberId);
        await _notificationHub.Clients.User(memberId).SendAsync("NotificationDismissed", id);
        return Ok(new { success = true });
    }

}
