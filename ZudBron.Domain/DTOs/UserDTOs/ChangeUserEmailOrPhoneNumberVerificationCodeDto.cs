using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ChangeUserEmailOrPhoneNumberVerificationCodeDto
    {
        [Required]
        public int Code { get; set; }
    }
}
