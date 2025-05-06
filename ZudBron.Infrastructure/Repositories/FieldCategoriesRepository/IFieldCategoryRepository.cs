using ZudBron.Domain.Models.FieldCategories;

namespace ZudBron.Infrastructure.Repositories.FieldCategoriesRepository
{
    public interface IFieldCategoryRepository
    {
        Task<List<FieldCategory>> GetAllAsync();
        Task<FieldCategory?> GetByIdAsync(Guid id);
        Task AddAsync(FieldCategory category);
        Task UpdateAsync(FieldCategory category);
        Task DeleteAsync(FieldCategory category);
    }

}
