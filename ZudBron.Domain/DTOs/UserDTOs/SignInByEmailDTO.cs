using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class SignInByEmailDto
    {
        [Required(ErrorMessage = "Email majburiy!")]
        [EmailAddress(ErrorMessage = "Email noto‘g‘ri formatda!")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Parol majburiy!")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak!")]
        public string Password { get; set; } = null!;
    }
}
