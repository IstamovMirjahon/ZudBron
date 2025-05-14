
using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.FieldDTO;

namespace ZudBron.Application.IService.IFieldServices
{
    public interface ISportFieldService
    {
        Task<Result<List<SportFieldDto>>> GetAllAsync();
        Task<Result<SportFieldDto?>> GetByIdAsync(Guid id);
        Task<Result<Guid>> CreateAsync(CreateOrUpdateSportFieldDto dto);
        Task<Result<bool>> UpdateAsync(Guid id, CreateOrUpdateSportFieldDto dto);
        Task<Result<bool>> DeleteAsync(Guid id);

        Task<Result<List<ReviewDto>>> GetReviewsBySportFieldIdAsync(Guid sportFieldId);

        Task<Result<List<SportFieldDto>>> FilterAsync(SportFieldFilterDto filter);


    }

}
