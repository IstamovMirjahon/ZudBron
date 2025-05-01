using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.NotificationEnum;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.NotificationModels
{
    public class Notification : BaseParams
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; } = null!;
        public NotificationType Type { get; set; } = NotificationType.InApp;
        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public virtual User User { get; set; } = null!;

        // Computed properties
        public bool IsUnread => !IsRead;
        public bool IsEmail => Type == NotificationType.Email;
        public bool IsSms => Type == NotificationType.SMS;
        public bool IsInApp => Type == NotificationType.InApp;
    }

}
