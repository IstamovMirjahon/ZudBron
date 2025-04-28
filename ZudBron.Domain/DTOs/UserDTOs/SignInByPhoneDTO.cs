using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class SignInByPhoneDto
    {
        [Required(ErrorMessage = "Phone majburiy!")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "Parol majburiy!")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak!")]
        public string Password { get; set; } = null!;
    }
}
