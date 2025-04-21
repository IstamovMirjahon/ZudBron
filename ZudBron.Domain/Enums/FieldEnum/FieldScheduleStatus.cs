namespace ZudBron.Domain.Enums.FieldEnum
{
    public enum FieldScheduleStatus
    {
        Available = 1,   // Hali hech kim band qilmagan
        Booked = 2,      // Foydalanuvchi tomonidan band qilingan
        Cancelled = 3,   // Band qilish bekor qilingan
        Completed = 4    // Belgilangan vaqt o‘tib bo‘lgan
    }
}
