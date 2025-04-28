using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.TokenDTOs
{
    public class RefreshTokenDto
    {
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
