using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ChangeUserFullNameDto
    {
        [Required(ErrorMessage = "Ism familya kiritilishi shart")]
        [MaxLength(100, ErrorMessage = "Ism familya 100 ta belgidan oshmasligi kerak")]
        public string FullName { get; set; } = null!;
    }
}
