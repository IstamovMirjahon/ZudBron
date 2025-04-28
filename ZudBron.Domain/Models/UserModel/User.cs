using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.UserEnum;
using ZudBron.Domain.Models.BookingModels;
using ZudBron.Domain.Models.Reviews;

namespace ZudBron.Domain.Models.UserModel
{
    public class User : BaseParams
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;

        public List<Booking> Bookings { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }
}
