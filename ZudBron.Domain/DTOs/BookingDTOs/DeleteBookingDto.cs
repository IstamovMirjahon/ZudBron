namespace ZudBron.Domain.DTOs.BookingDTOs
{
    public class DeleteBookingDto
    {
        public Guid Id { get; set; }
        public string? Reason { get; set; } // ❌ Foydalanuvchi o‘zi bekor qildi
        public string? BookingCode { get; set; } // ❌ Foydalanuvchi o‘zi bekor qildi

    }
}
