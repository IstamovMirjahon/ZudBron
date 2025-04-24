using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Models.Media;
using ZudBron.Domain.Models.SportFieldModels;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.Reviews
{
    public class Review : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        [Required]
        public Guid SportFieldId { get; set; }
        public SportField SportField { get; set; } = default!;

        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime Created { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        // MediaFile qo‘shilsa, sharhga rasm/video biriktirish mumkin
        public Guid? MediaFileId { get; set; }
        public MediaFile? MediaFile { get; set; }

    }
}
