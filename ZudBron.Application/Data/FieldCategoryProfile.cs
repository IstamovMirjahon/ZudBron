using AutoMapper;
using ZudBron.Domain.DTOs.FieldCategories;
using ZudBron.Domain.Models.FieldCategories;

namespace ZudBron.Application.Data
{
    public class FieldCategoryProfile : Profile
    {
        public FieldCategoryProfile()
        {
            CreateMap<FieldCategory, FieldCategoryDto>();
            CreateMap<CreateFieldCategoryDto, FieldCategory>();
            CreateMap<UpdateFieldCategoryDto, FieldCategory>().ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}
