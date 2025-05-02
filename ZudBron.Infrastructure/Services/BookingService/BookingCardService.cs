using ZudBron.Application.IService.IBookingService;
using ZudBron.Domain.DTOs.BookingDTOs;
using ZudBron.Domain.Enums.BookingEnum;
using ZudBron.Infrastructure.Repositories.BookingRepositories;

namespace ZudBron.Infrastructure.Services.BookingService
{
    public class BookingCardService : IBookingCardService
    {
        private readonly IBookingCardRepository _bookingRepository;

        public BookingCardService(IBookingCardRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<BookingCardDto>> GetUserBookingsAsync(Guid userId)
        {
            var bookings = await _bookingRepository.GetUserBookingsAsync(userId);

            return bookings.Select(b => new BookingCardDto
            {
                Id = b.Id,
                Title = b.SportField.Name ?? "",
                Day = b.StartDate.Date,
                Time = $"{b.StartDate:HH:mm} - {b.EndDate:HH:mm}",
                Location = b.SportField.Location.Country + b.SportField.Location.Region + b.SportField.Location.City + b.SportField.Location.AddressLine,
                Payed = b.PaymentStatus == PaymentStatus.Paid ? b.SportField.PricePerHour : 0,
                Left = b.PaymentStatus == PaymentStatus.Paid ? 0 : b.SportField.PricePerHour,
                IsArchived = b.EndDate <= DateTime.UtcNow
            }).ToList();
        }

        public async Task<BookingCardDto?> GetBookingDetailsAsync(Guid bookingId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);

            if (booking is null)
                return null;

            return new BookingCardDto
            {
                Id = booking.Id,
                Title = booking.SportField.Name,
                Day = booking.StartDate.Date,
                Time = $"{booking.StartDate:HH:mm} - {booking.EndDate:HH:mm}",
                Location = booking.SportField.Location.Country + booking.SportField.Location.Region + booking.SportField.Location.City + booking.SportField.Location.AddressLine,

                Payed = booking.PaymentStatus == PaymentStatus.Paid ? booking.SportField.PricePerHour : 0,
                Left = booking.PaymentStatus == PaymentStatus.Paid ? 0 : booking.SportField.PricePerHour,
                IsArchived = booking.EndDate <= DateTime.UtcNow
            };
        }
    }

}
