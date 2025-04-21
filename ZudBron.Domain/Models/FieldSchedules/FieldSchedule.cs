using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Enums.FieldEnum;
using ZudBron.Domain.Models.Media;
using ZudBron.Domain.Models.UserModel;

namespace ZudBron.Domain.Models.FieldSchedules
{
    public class FieldSchedule : BaseEntity
    {
        public Guid SportFieldId { get; set; }
        //public SportField SportField { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsBooked { get; set; } = false;
        public FieldScheduleStatus Status { get; set; } = FieldScheduleStatus.Available;

        public Guid? BookedByUserId { get; set; }
        public User? BookedByUser { get; set; }

        public string? Description { get; set; }

        public ICollection<MediaFile> MediaFiles { get; set; } = new List<MediaFile>();
    }
    
}
