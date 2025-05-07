using ZudBron.Domain.DTOs.FieldCategories;

namespace ZudBron.Application.IService.IFieldCategories
{
    public interface IFieldCategoryService
    {
        Task<List<FieldCategoryDto>> GetAllCategoriesAsync();
        Task<FieldCategoryDto?> GetCategoryByIdAsync(Guid id);
        Task CreateCategoryAsync(CreateFieldCategoryDto dto);
        Task UpdateCategoryAsync(UpdateFieldCategoryDto dto);
        Task DeleteCategoryAsync(Guid id);
    }

}
