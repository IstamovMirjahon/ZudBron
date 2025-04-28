using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;

namespace ZudBron.Domain.Models.UserModel
{
    public class RefreshToken : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
