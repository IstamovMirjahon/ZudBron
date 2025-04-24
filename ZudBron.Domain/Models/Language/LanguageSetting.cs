using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.SettingsModels
{
    public class LanguageSetting : BaseParams
    {
        public Guid UserId { get; set; } // Bog‘liq foydalanuvchi
        public virtual User User { get; set; } = null!;

        public string LanguageCode { get; set; } = "uz";       // "uz", "ru", "en"
        public string CountryCode { get; set; } = "UZ";        // "UZ", "RU", "US"
        public string CurrencyCode { get; set; } = "UZS";      // "UZS", "RUB", "USD"
        public string TimeZone { get; set; } = "Asia/Tashkent";// "Asia/Tashkent", "Europe/Moscow"
        public string DateFormat { get; set; } = "dd/MM/yyyy"; // "dd/MM/yyyy", "MM/dd/yyyy"

        public bool IsRTL => LanguageCode is "ar" or "fa"; // Right-To-Left tillar (masalan: arabcha)
    }
}
