using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.BookingEnum;
using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Domain.Models.PaymentModels
{
    public class Payment : BaseParams
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }               // Foydalanuvchi tomonidan to‘langan miqdor
        public decimal TotalAmount { get; set; }          // Umumiy kerakli to‘lov (masalan: 200,000 so‘m)
        public decimal MinimumRequiredAmount { get; set; } // Minimal to‘lash kerak bo‘lgan summa (masalan: avans = 20% bo‘lsa 40,000)

        public string Currency { get; set; } = "UZS";     // Valyuta

        public string PaymentMethod { get; set; } = null!;  // Masalan: "Click", "Payme", "Cash"
        public string? PaymentType { get; set; }            // Masalan: "Online", "Offline", "AdminPanel"
        public string? PaymentReference { get; set; }       // Tashqi tizimdan kelgan ID
        public string? PaymentCode { get; set; }            // Foydalanuvchiga ko‘rsatiladigan kod (masalan: chek raqami)
        public string? Description { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public bool IsRefunded { get; set; } = false;
        public bool IsCancelled { get; set; } = false;

        // Computed properties
        public bool IsCompleted => PaymentStatus == PaymentStatus.Paid;
        public bool IsPartiallyPaid => PaymentStatus == PaymentStatus.PartiallyPaid;
        public bool IsUnpaid => PaymentStatus == PaymentStatus.Unpaid;
        public bool IsFailed => PaymentStatus == PaymentStatus.Failed;
        public bool IsRefundedOrCancelled => PaymentStatus == PaymentStatus.Refunded || PaymentStatus == PaymentStatus.Cancelled;

        // Navigation
        public virtual Booking Booking { get; set; } = null!;
    }
}
