using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class NotificationEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [ForeignKey(nameof(NotificationType))]
    public int NotificationTypeId { get; set; }
    public NotificationTypeEntity NotificationType { get; set; } = null!;

    [ForeignKey(nameof(TargetGroup))]
    public int TargetGroupId { get; set; } = 1;
    public NotificationTargetEntity TargetGroup { get; set; } = null!;

    public string Image { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public ICollection<NotificationDismissEntity> DismissedNotifications { get; set; } = [];

}
