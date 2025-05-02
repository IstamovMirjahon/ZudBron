using System.ComponentModel.DataAnnotations;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.FieldEnum;
using ZudBron.Domain.Models.FieldSchedules;
using ZudBron.Domain.Models.Media;
using ZudBron.Domain.Models.Reviews;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.SportFieldModels
{
    public class SportField : BaseParams
    {
        [Required(ErrorMessage = "Maydon nomi majburiy")]
        [MaxLength(100, ErrorMessage = "Maydon nomi 100 belgidan oshmasligi kerak")]
        public string? Name { get; set; }

        [Range(0, 1000000, ErrorMessage = "Narx 0 dan katta bo‘lishi kerak")]
        public decimal PricePerHour { get; set; }

        [MaxLength(1000, ErrorMessage = "Tavsif 1000 belgidan oshmasligi kerak")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Ochilish vaqti majburiy")]
        public TimeSpan OpenHour { get; set; }

        [Required(ErrorMessage = "Yopilish vaqti majburiy")]
        public TimeSpan CloseHour { get; set; }

        [Required(ErrorMessage = "Joylashuv ID majburiy")]
        public Guid LocationId { get; set; }

        public Location? Location { get; set; }

        [Required(ErrorMessage = "Kategoriya majburiy")]
        public FieldCategory Category { get; set; }

        [Required(ErrorMessage = "Egasining ID raqami majburiy")]
        public Guid OwnerId { get; set; }

        public User? Owner { get; set; }

        public List<FieldSchedule>? Schedules { get; set; }

        public List<Review>? Reviews { get; set; }

        public List<MediaFile>? MediaFiles { get; set; }
    }
}
