
namespace ZudBron.Domain.DTOs.FieldDTO
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string AuthorFullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? MediaUrl { get; set; }
    }

}
