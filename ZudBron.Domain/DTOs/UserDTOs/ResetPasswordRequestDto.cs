using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ResetPasswordRequestDto
    {
        [Required(ErrorMessage = "Email manzili kiritilishi shart")]
        [EmailAddress(ErrorMessage = "Email formati noto‘g‘ri")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Parol kiritilishi shart")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak")]
        public string NewPassword { get; set; } = null!;

        [Compare(nameof(NewPassword), ErrorMessage = "Parollar mos emas")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
