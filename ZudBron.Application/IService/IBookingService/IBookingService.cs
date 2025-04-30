using Article.Domain.Abstractions;
using ZudBron.Domain.DTOs.BookingDTOs;
using ZudBron.Domain.Models.BookingModels;

namespace ZudBron.Application.IService.IBookingService
{
    public interface IBookingService
    {
        Task<Result<BookingResponseDto>> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<BookingResponseDto>>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<BookingResponseDto>>> GetBookingsBySportFieldIdAsync(Guid sportFieldId, CancellationToken cancellationToken = default);
        Task<Result<BookingResponseDto>> AddBookingAsync(CreateBookingDto booking, Guid userId, CancellationToken cancellationToken = default);
        Task<Result<BookingResponseDto>> UpdateBookingAsync(UpdateBookingDto booking, CancellationToken cancellationToken = default);
        Task<Result<BookingResponseDto>> DeleteBookingAsync(DeleteBookingDto deleteBookingDto, CancellationToken cancellationToken = default);
    }
}
