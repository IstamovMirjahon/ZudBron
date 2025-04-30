using AutoMapper;
using ZudBron.Domain.DTOs.BookingDTOs;
using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Application.Data
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<Booking, BookingResponseDto>();
        }
    }

}
