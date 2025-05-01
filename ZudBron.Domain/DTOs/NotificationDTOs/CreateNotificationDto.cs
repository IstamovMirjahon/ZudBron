

namespace ZudBron.Domain.DTOs.NotificationDTOs
{
    public class CreateNotificationDto
    {
        public Guid UserId { get; set; }
        public string Message { get; set; } = null!;
        public string Type { get; set; } = null!; // "InApp", "Email", "SMS"
    }
}
