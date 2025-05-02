using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class RegisterVerificationCodeByPhoneDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public int Code { get; set; }
    }
}
