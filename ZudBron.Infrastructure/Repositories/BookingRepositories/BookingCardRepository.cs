using Microsoft.EntityFrameworkCore;
using System;
using ZudBron.Domain.Enums.BookingEnum;
using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Infrastructure.Repositories.BookingRepositories
{
    public class BookingCardRepository : IBookingCardRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingCardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetUserBookingsAsync(Guid userId)
        {
            return await _context.Bookings
                .Include(b => b.SportField)
                .Where(b => b.UserId == userId && b.BookingStatus != BookingStatus.Cancelled)
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.SportField)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
    }

}
