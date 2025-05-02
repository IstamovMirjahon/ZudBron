using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ForgotPasswordByPhoneDto
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
