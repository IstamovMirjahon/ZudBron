using ZudBron.Domain.DTOs.BookingDTOs;

namespace ZudBron.Application.IService.IBookingService
{
    public interface IBookingCardService
    {
        Task<List<BookingCardDto>> GetUserBookingsAsync(Guid userId);
        Task<BookingCardDto?> GetBookingDetailsAsync(Guid bookingId);
    }
}
