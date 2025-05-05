using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ChangeUserEmailDto
    {
        [Required(ErrorMessage = "Email manzili kiritilishi shart")]
        [EmailAddress(ErrorMessage = "Email formati noto‘g‘ri")]
        public string Email { get; set; } = null!;
    }
}
