using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.NotificationEnum;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.NotificationModels
{
    public class Notification : BaseParams
    {
        [Required(ErrorMessage = "Foydalanuvchi ID majburiy")]
        public Guid UserId { get; set; }

        [MaxLength(250, ErrorMessage = "Sarlavha 250 belgidan oshmasligi kerak")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Xabar matni majburiy")]
        [MaxLength(1000, ErrorMessage = "Xabar 1000 belgidan oshmasligi kerak")]
        public string Message { get; set; } = null!;

        [Required]
        public NotificationType Type { get; set; } = NotificationType.InApp;

        public bool IsRead { get; set; } = false;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Required]
        public virtual User User { get; set; } = null!;

        // Computed properties
        public bool IsUnread => !IsRead;
        public bool IsEmail => Type == NotificationType.Email;
        public bool IsSms => Type == NotificationType.SMS;
        public bool IsInApp => Type == NotificationType.InApp;
    }
}
