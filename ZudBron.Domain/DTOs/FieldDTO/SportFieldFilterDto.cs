
using ZudBron.Domain.Models.FieldCategories;

namespace ZudBron.Domain.DTOs.FieldDTO
{
    public class SportFieldFilterDto
    {
        public string? Name { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public TimeSpan? DesiredStartTime { get; set; }
        public TimeSpan? DesiredEndTime { get; set; }
    }

}
