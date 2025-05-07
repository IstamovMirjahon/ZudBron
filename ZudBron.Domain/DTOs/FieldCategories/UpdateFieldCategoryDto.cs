using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.FieldCategories
{
    public class UpdateFieldCategoryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
}
