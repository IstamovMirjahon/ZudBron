using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class UserRegisterByEmailDto
    {
        [Required(ErrorMessage = "Ism familya kiritilishi shart")]
        [MaxLength(100, ErrorMessage = "Ism familya 100 ta belgidan oshmasligi kerak")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email manzili kiritilishi shart")]
        [EmailAddress(ErrorMessage = "Email formati noto‘g‘ri")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Parol kiritilishi shart")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = "Parollar mos emas")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
