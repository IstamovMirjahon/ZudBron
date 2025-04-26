using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class RegisterVerificationCodeDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Code { get; set; }
    }
}
