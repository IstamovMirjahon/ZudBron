using Article.Domain.Abstractions;
using AutoMapper;
using ZudBron.Application.IService.IBookingService;
using ZudBron.Domain.Abstractions;
using ZudBron.Domain.DTOs.BookingDTOs;
using ZudBron.Domain.Enums.BookingEnum;
using ZudBron.Domain.Models.BookingModels;
using ZudBron.Infrastructure.Repositories.BookingRepositories;

namespace ZudBron.Infrastructure.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BookingResponseDto>> GetBookingByIdAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId, cancellationToken);
            if (booking is null)
                return Result<BookingResponseDto>.Failure(new Error("Booking.NotFound", "Booking topilmadi"));

            var dto = _mapper.Map<BookingResponseDto>(booking);
            return Result<BookingResponseDto>.Success(dto);
        }

        public async Task<Result<IEnumerable<BookingResponseDto>>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId, cancellationToken);
            var dtoList = _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
            return Result<IEnumerable<BookingResponseDto>>.Success(dtoList);
        }

        public async Task<Result<IEnumerable<BookingResponseDto>>> GetBookingsBySportFieldIdAsync(Guid sportFieldId, CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetBookingsBySportFieldIdAsync(sportFieldId, cancellationToken);
            var dtoList = _mapper.Map<IEnumerable<BookingResponseDto>>(bookings);
            return Result<IEnumerable<BookingResponseDto>>.Success(dtoList);
        }

        public async Task<Result<BookingResponseDto>> AddBookingAsync(CreateBookingDto bookingDto, Guid userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    SportFieldId = bookingDto.SportFieldId,
                    StartDate = bookingDto.StartDate,
                    EndDate = bookingDto.EndDate,
                    Description = bookingDto.Description,
                    BookingType = bookingDto.BookingType,
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    BookingStatus = BookingStatus.Pending,
                    PaymentStatus = PaymentStatus.Unpaid
                };

                await _bookingRepository.AddBookingAsync(booking, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var dto = _mapper.Map<BookingResponseDto>(booking);
                return Result<BookingResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<BookingResponseDto>.Failure(new Error("Booking.Create", ex.Message));
            }
        }

        public async Task<Result<BookingResponseDto>> UpdateBookingAsync(UpdateBookingDto bookingDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingBooking = await _bookingRepository.GetBookingByIdAsync(bookingDto.Id, cancellationToken);

                if (existingBooking == null)
                    return Result<BookingResponseDto>.Failure(new Error("Booking.NotFound", "Booking topilmadi."));

                existingBooking.StartDate = bookingDto.StartDate;
                existingBooking.EndDate = bookingDto.EndDate;
                existingBooking.Description = bookingDto.Description;
                existingBooking.BookingType = bookingDto.BookingType;
                existingBooking.UpdateDate = DateTime.UtcNow;

                await _bookingRepository.UpdateBookingAsync(existingBooking, cancellationToken);
                var dto = _mapper.Map<BookingResponseDto>(existingBooking);
                return Result<BookingResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<BookingResponseDto>.Failure(new Error("Booking.Update", ex.Message));
            }
        }

        public async Task<Result<BookingResponseDto>> DeleteBookingAsync(DeleteBookingDto deleteBookingDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingByIdAsync(deleteBookingDto.Id, cancellationToken);
                if (booking is null)
                    return Result<BookingResponseDto>.Failure(new Error("Booking.NotFound", "Booking topilmadi."));

                await _bookingRepository.DeleteBookingAsync(deleteBookingDto.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var dto = _mapper.Map<BookingResponseDto>(booking);
                return Result<BookingResponseDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<BookingResponseDto>.Failure(new Error("Booking.Delete", ex.Message));
            }
        }
    }
}
