
using ZudBron.Domain.Enums.BookingEnum;

namespace ZudBron.Domain.DTOs.PaymentDTOs
{
    public class ClickPrepareDto
    {
        public int Click_trans_id { get; set; }
        public int Service_id { get; set; }
        public string Merchant_trans_id { get; set; }
        public string Merchant_prepare_id { get; set; }
        public int Amount { get; set; }
        public int Action { get; set; }
        public int Error { get; set; }
        public PaymentStatus Error_note { get; set; }
        public string Sign_time { get; set; }
        public string Sign_string { get; set; }
    }

}
