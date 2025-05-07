using ZudBron.Domain.Abstractions;
using ZudBron.Domain.Models.SportFieldModels;

namespace ZudBron.Domain.Models.FieldCategories
{
    public class FieldCategory : BaseParams
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        public virtual ICollection<SportField> SportFields { get; set; } = new List<SportField>();
    }

}
