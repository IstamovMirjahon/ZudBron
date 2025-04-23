namespace ZudBron.Domain.Enums.BookingEnum
{
    public enum BookingStatus
    {
        Pending,     // 🕒 So‘rov yuborilgan, hali tasdiqlanmagan
        Confirmed,   // ✅ Tasdiqlandi (maydon egasi tomonidan)
        Cancelled    // ❌ Foydalanuvchi o‘zi bekor qildi
    }
}
