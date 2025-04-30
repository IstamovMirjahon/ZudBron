using Microsoft.EntityFrameworkCore;
using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Infrastructure.Repositories.BookingRepositories
{

    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Booking?> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Include(b => b.SportField)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);
        }
        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Include(b => b.SportField)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<Booking>> GetBookingsBySportFieldIdAsync(Guid sportFieldId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Include(b => b.SportField)
                .Include(b => b.User)
                .Where(b => b.SportFieldId == sportFieldId)
                .ToListAsync(cancellationToken);
        }
        public async Task AddBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            await _context.Bookings.AddAsync(booking, cancellationToken);
        }
        public async Task UpdateBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteBookingAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await GetBookingByIdAsync(bookingId, cancellationToken);

            if (booking == null)
                throw new InvalidOperationException($"Booking with ID {bookingId} not found.");

            _context.Bookings.Remove(booking);
        }

    }
}
