

using ZudBron.Domain.Enums.BookingEnum;

namespace ZudBron.Domain.Models.PaymentModels
{
    public class PaymentClick
    {
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
        public string MerchantTransactionId { get; set; }
        public string MerchantPrepareId { get; set; }
        public int Amount { get; set; }
        public PaymentStatus Status { get; set; } // Pending, Success, Failed
        public DateTime CreatedAt { get; set; }
    }
}
