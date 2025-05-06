using System.ComponentModel.DataAnnotations;

namespace ZudBron.Domain.DTOs.FieldCategories
{
    public class CreateFieldCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
