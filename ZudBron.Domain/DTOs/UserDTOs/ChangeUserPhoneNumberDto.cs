using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.UserDTOs
{
    public class ChangeUserPhoneNumberDto
    {
        [Required(ErrorMessage = "PhoneNumber manzili kiritilishi shart")]
        [Phone(ErrorMessage = "PhoneNumber formati noto‘g‘ri")]
        public string PhoneNumber { get; set; } = null!;
    }
}
