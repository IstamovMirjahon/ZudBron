namespace ZudBron.Domain.DTOs.BookingDTOs
{
    public class CreateBookingDto
    {
        public Guid SportFieldId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? BookingType { get; set; }
    }

}
