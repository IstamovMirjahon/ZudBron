

namespace ZudBron.Domain.DTOs.NotificationDTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; } = null!;
        public string Type { get; set; } = "InApp";
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; } // Qo‘shildi
    }

}
