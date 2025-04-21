using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class UserRegisterByPhoneDto
    {
        [Required(ErrorMessage = "Ism familya kiritilishi shart")]
        [MaxLength(100, ErrorMessage = "Ism familya 100 ta belgidan oshmasligi kerak")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Telefon raqam kiritilishi shart")]
        [Phone(ErrorMessage = "Telefon raqam noto‘g‘ri formatda")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Parol kiritilishi shart")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = "Parollar mos emas")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
