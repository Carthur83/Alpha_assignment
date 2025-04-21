using Business.Hubs;
using Business.Interfaces;
using Data.Contexts;
using Data.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class NotificationService(DataContext context, IHubContext<NotificationHub> notificationHub) : INotificationService
{
    private readonly DataContext _context = context;
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;

    public async Task AddNotificationAsync(NotificationEntity notificationEntity, string userId = "anonymous")
    {
        if (string.IsNullOrEmpty(notificationEntity.Image))
        {
            switch (notificationEntity.NotificationTypeId)
            {
                case 1:
                    notificationEntity.Image = "UserImage1.svg";
                    break;

                case 2:
                    notificationEntity.Image = "ProjectImage.svg";
                    break;
            }
        }

        _context.Add(notificationEntity);
        await _context.SaveChangesAsync();

        var notifications = await GetNotificationsAsync(userId);
        var newNotification = notifications.OrderByDescending(x => x.Created).FirstOrDefault();

        if (newNotification != null)
        {
            await _notificationHub.Clients.All.SendAsync("ReceiveNotification", newNotification);
        }
    }

    public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string memberId, int take = 5)
    {
        var dismissedIds = await _context.DismissedNotifications
            .Where(x => x.MemberId == memberId)
            .Select(x => x.NotificationId)
            .ToListAsync();

        var notifications = await _context.Notifications
            .Where(x => !dismissedIds.Contains(x.Id))
            .OrderByDescending(x => x.Created)
            .Take(take)
            .ToListAsync();

        return notifications;
    }

    public async Task DismissNotificationAsync(string notificationId, string memberId)
    {
        var alreadyDismissed = await _context.DismissedNotifications.AnyAsync(x => x.NotificationId == notificationId && x.MemberId == memberId);
        if (!alreadyDismissed)
        {
            var dismissed = new NotificationDismissEntity
            {
                NotificationId = notificationId,
                MemberId = memberId
            };

            _context.Add(dismissed);
            await _context.SaveChangesAsync();
        }
    }
}
