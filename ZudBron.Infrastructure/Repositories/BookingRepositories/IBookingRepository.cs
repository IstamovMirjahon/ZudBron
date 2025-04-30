using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Infrastructure.Repositories.BookingRepositories
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Booking>> GetBookingsBySportFieldIdAsync(Guid sportFieldId, CancellationToken cancellationToken = default);
        Task AddBookingAsync(Booking booking, CancellationToken cancellationToken = default);
        Task UpdateBookingAsync(Booking booking, CancellationToken cancellationToken = default);
        Task DeleteBookingAsync(Guid bookingId, CancellationToken cancellationToken = default);
    }
}
