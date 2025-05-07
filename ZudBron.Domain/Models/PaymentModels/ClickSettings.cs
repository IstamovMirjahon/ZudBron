using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.Models.PaymentModels
{
    public class ClickSettings
    {
        [Required(ErrorMessage = "ServiceId is required.")]
        [StringLength(50, ErrorMessage = "ServiceId cannot exceed 50 characters.")]
        public string ServiceId { get; set; }

        [Required(ErrorMessage = "MerchantId is required.")]
        [StringLength(50, ErrorMessage = "MerchantId cannot exceed 50 characters.")]
        public string MerchantId { get; set; }

        [Required(ErrorMessage = "SecretKey is required.")]
        [StringLength(100, ErrorMessage = "SecretKey cannot exceed 100 characters.")]
        public string SecretKey { get; set; }

        [Required(ErrorMessage = "MerchantUserId is required.")]
        [StringLength(50, ErrorMessage = "MerchantUserId cannot exceed 50 characters.")]
        public string MerchantUserId { get; set; }

        [Required(ErrorMessage = "BaseUrl is required.")]
        [Url(ErrorMessage = "BaseUrl must be a valid URL.")]
        public string BaseUrl { get; set; }

        [Required(ErrorMessage = "ReturnUrl is required.")]
        [Url(ErrorMessage = "ReturnUrl must be a valid URL.")]
        public string ReturnUrl { get; set; }
    }
}
