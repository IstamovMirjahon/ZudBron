
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
        public string? Name { get; set; }
        public decimal PricePerHour { get; set; }
        public string? Description { get; set; }
        public TimeSpan OpenHour { get; set; }
        public TimeSpan CloseHour { get; set; }
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }


        public FieldCategory Category { get; set; }
        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }

        public List<FieldSchedule>? Schedules { get; set; }
        public List<Review>? Reviews { get; set; }

        public List<MediaFile>? MediaFiles { get; set; }
    }

}
