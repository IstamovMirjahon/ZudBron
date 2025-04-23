
namespace ZudBron.Domain.DTOs.FieldDTO
{
    public class SportFieldDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal PricePerHour { get; set; }
        public string? Description { get; set; }
        public TimeSpan OpenHour { get; set; }
        public TimeSpan CloseHour { get; set; }
        public Guid LocationId { get; set; }
        public string? LocationName { get; set; } // optional

        public string? Category { get; set; }
        public Guid OwnerId { get; set; }
        public string? OwnerFullName { get; set; }

        public List<string>? MediaUrls { get; set; }
        public double? AverageRating { get; set; } // if needed
    }

}
