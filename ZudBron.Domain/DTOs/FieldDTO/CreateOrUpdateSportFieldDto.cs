
namespace ZudBron.Domain.DTOs.FieldDTO
{
    public class CreateOrUpdateSportFieldDto
    {
        public string? Name { get; set; }
        public decimal PricePerHour { get; set; }
        public string? Description { get; set; }
        public TimeSpan OpenHour { get; set; }
        public TimeSpan CloseHour { get; set; }
        public Guid LocationId { get; set; }

        public string? Category { get; set; }
        public Guid OwnerId { get; set; }

        public List<Guid>? MediaFileIds { get; set; } // Fayllar oldindan yuklab qo‘yilgan bo‘lsa
    }

}
