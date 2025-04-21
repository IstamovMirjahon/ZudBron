using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.Models.UserModel
{
    public class TempUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string HashedPassword { get; set; } = null!;

        public string VerificationCode { get; set; } = null!;

        public DateTime ExpirationTime { get; set; } = DateTime.UtcNow.AddMinutes(2);
    }
}
