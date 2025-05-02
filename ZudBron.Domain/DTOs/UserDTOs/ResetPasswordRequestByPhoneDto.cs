using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ResetPasswordRequestByPhoneDto
    {
        [Required(ErrorMessage = "Email manzili kiritilishi shart")]
        [Phone(ErrorMessage = "PhoneNumber formati noto‘g‘ri")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Parol kiritilishi shart")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak")]
        public string NewPassword { get; set; } = null!;

        [Compare(nameof(NewPassword), ErrorMessage = "Parollar mos emas")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
