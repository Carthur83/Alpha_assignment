using Data.Entities;
using Microsoft.Identity.Client;

namespace Business.Interfaces;

public interface INotificationService
{
    Task AddNotificationAsync(NotificationEntity notificationEntity, string userId);
    Task DismissNotificationAsync(string notificationId, string memberId);
    Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string memberId, int take = 5);
}