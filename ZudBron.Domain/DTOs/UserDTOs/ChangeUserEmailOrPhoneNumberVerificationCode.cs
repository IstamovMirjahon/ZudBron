using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ChangeUserEmailOrPhoneNumberVerificationCode
    {
        [Required]
        public int Code { get; set; }
    }
}
