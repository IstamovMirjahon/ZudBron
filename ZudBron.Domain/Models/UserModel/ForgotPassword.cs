using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;

namespace ZudBron.Domain.Models.UserModel
{
    public class ForgotPassword : BaseEntity
    {
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        public int VerificationCode { get; set; }
        public DateTime ExpirationTime { get; set; } = DateTime.UtcNow.AddMinutes(2);
        public bool IsUsed { get; set; } = false;
    }
}
