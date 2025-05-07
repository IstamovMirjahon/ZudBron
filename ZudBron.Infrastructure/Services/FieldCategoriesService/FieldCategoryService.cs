using AutoMapper;
using ZudBron.Application.IService.IFieldCategories;
using ZudBron.Domain.DTOs.FieldCategories;
using ZudBron.Domain.Models.FieldCategories;
using ZudBron.Infrastructure.Repositories.FieldCategoriesRepository;

namespace ZudBron.Infrastructure.Services.FieldCategoriesService
{
    public class FieldCategoryService : IFieldCategoryService
    {
        private readonly IFieldCategoryRepository _repository;
        private readonly IMapper _mapper;

        public FieldCategoryService(IFieldCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FieldCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return _mapper.Map<List<FieldCategoryDto>>(categories);
        }

        public async Task<FieldCategoryDto?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category is null ? null : _mapper.Map<FieldCategoryDto>(category);
        }

        public async Task CreateCategoryAsync(CreateFieldCategoryDto dto)
        {
            var category = _mapper.Map<FieldCategory>(dto);
            await _repository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(UpdateFieldCategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(dto.Id);
            if (category is null) throw new Exception("Category not found");

            _mapper.Map(dto, category);
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) throw new Exception("Category not found");

            await _repository.DeleteAsync(category);
        }
    }

}
