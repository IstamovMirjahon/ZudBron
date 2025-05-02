using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ForgotPasswordVerificationCodeByPhoneDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public int Code { get; set; }
    }
}
