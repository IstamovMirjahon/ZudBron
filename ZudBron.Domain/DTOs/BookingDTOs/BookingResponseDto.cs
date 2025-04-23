using ZudBron.Domain.Enums.BookingEnum;

namespace ZudBron.Domain.DTOs.BookingDTOs
{
    public class BookingResponseDto
    {
        public Guid Id { get; set; }
        public string? SportFieldName { get; set; }
        public string? UserFullName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string? BookingCode { get; set; }
    }

}
