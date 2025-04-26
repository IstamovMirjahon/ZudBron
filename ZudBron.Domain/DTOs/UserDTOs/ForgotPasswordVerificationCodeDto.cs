using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ForgotPasswordVerificationCodeDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Code { get; set; }
    }
}
