namespace ZudBron.Domain.DTOs.BookingDTOs
{
    public class BookingCardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;           // SportField.Name
        public DateTime Day { get; set; }                    // StartDate.Date
        public string Time { get; set; } = null!;            // "HH:mm - HH:mm"
        public string Location { get; set; } = null!;        // SportField.Location

        public decimal Payed { get; set; }                   // Paid qismi
        public decimal Left { get; set; }                    // Qolgan to‘lov

        public bool IsArchived { get; set; }                 // EndDate < DateTime.UtcNow
    }

}
