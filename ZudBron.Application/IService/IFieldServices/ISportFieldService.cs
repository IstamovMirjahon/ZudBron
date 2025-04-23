
using ZudBron.Domain.DTOs.FieldDTO;

namespace ZudBron.Application.IService.IFieldServices
{
    public interface ISportFieldService
    {
        Task<List<SportFieldDto>> GetAllAsync();
        Task<SportFieldDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateOrUpdateSportFieldDto dto);
        Task<bool> UpdateAsync(Guid id, CreateOrUpdateSportFieldDto dto);
        Task<bool> DeleteAsync(Guid id);

        Task<List<ReviewDto>> GetReviewsBySportFieldIdAsync(Guid sportFieldId);

        Task<List<SportFieldDto>> FilterAsync(SportFieldFilterDto filter);


    }

}
