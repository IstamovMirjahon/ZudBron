using ZudBron.Domain.Abstractions;
using ZudBron.Domain.DTOs.UserDTOs;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.NotificationModels
{
    public class Notification : BaseParams
    {
        public Guid UserId { get; set; }                   // Qaysi foydalanuvchiga yuborilgan
        public string Message { get; set; } = null!;       // Xabar matni
        public NotificationType Type { get; set; }         // Email, SMS yoki InApp
        public DateTime SentAt { get; set; }               // Yuborilgan vaqt
        public bool IsRead { get; set; } = false;          // O‘qilganligi (false - unread)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Yaratilgan vaqt

        // Computed properties
        public bool IsUnread => !IsRead;
        public bool IsEmail => Type == NotificationType.Email;
        public bool IsSms => Type == NotificationType.SMS;
        public bool IsInApp => Type == NotificationType.InApp;

        // Navigation
        public virtual User User { get; set; } = null!;
    }
}
