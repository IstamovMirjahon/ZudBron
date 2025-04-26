using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.TokenDTOs
{
    public class RefreshTokenDTO
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
