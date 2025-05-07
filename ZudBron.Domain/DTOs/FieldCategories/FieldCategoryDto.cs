namespace ZudBron.Domain.DTOs.FieldCategories
{
    public class FieldCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}
