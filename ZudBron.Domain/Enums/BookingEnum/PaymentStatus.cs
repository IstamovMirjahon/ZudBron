namespace ZudBron.Domain.Enums.BookingEnum
{
    public enum PaymentStatus
    {
        Unpaid,         // Hech qanday to‘lov qilinmagan
        PartiallyPaid,  // Qisman to‘lov qilingan (masalan: avans)
        Pending,        // kurilmoqda 
        Paid,           // To‘liq to‘lov amalga oshirilgan
        Refunded,       // To‘lov qaytarilgan
        Cancelled,      // To‘lov bekor qilingan
        Failed          // To‘lov amalga oshmagan
    }
}
