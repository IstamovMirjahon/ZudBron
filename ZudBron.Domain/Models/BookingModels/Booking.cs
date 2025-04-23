using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.BookingEnum;
using ZudBron.Domain.Enums.FieldEnum;
using ZudBron.Domain.Models.SportFieldModels;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.BookingModels
{
    public class Booking : BaseParams
    {
        public Guid SportFieldId { get; set; }
        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }     // Boshlanish vaqti
        public DateTime EndDate { get; set; }       // Tugash vaqti

        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        public string? PaymentMethod { get; set; }      // Masalan: "Payme", "Click", "Cash"
        public string? PaymentReference { get; set; }   // To‘lov tizimi qaytargan ID

        public string? Description { get; set; }        // Izoh
        public string? BookingReference { get; set; }   // Tashqi tizimlar uchun
        public string? BookingCode { get; set; }        // Foydalanuvchiga yuboriladigan short kod

        public string? BookingType { get; set; }        // Masalan: "Online", "AdminPanel"
        public FieldCategory? BookingCategory { get; set; }    // Masalan: "Futbol", "Basketbol" (future proof)

        public bool IsCancelled => BookingStatus == BookingStatus.Cancelled;
        public bool IsConfirmed => BookingStatus == BookingStatus.Confirmed;
        public bool IsPending => BookingStatus == BookingStatus.Pending;

        // Soft way of completion (event or cron orqali aniqlanadi)
        public bool IsCompleted => EndDate <= DateTime.UtcNow;

        // Navigation
        public virtual SportField SportField { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
