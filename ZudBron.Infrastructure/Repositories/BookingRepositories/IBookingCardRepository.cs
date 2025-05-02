using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Infrastructure.Repositories.BookingRepositories
{
    public interface IBookingCardRepository
    {
        Task<List<Booking>> GetUserBookingsAsync(Guid userId);
        Task<Booking?> GetBookingByIdAsync(Guid bookingId);
    }
}
