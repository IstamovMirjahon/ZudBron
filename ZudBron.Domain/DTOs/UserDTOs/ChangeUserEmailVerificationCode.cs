using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ChangeUserEmailVerificationCode
    {
        [Required]
        public int Code { get; set; }
    }
}
