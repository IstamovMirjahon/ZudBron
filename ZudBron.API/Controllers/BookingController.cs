using Microsoft.AspNetCore.Mvc;
using ZudBron.Application.IService.IBookingService;
using ZudBron.Domain.DTOs.BookingDTOs;

namespace ZudBron.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/Booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var bookingResult = await _bookingService.GetBookingByIdAsync(id);
            if (!bookingResult.IsSuccess)
                return NotFound(bookingResult.Error?.Message);

            return Ok(bookingResult.Value);
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto booking)
        {
            if (booking == null)
                return BadRequest("Booking cannot be null");

            var result = await _bookingService.AddBookingAsync(booking, Guid.NewGuid()); // userId ni o‘zingiz aniqlang

            if (!result.IsSuccess)
                return BadRequest(result.Error?.Message);

            return CreatedAtAction(nameof(GetBooking), new { id = result.Value?.Id }, result.Value);
        }

        // PUT: api/Booking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] UpdateBookingDto booking)
        {
            if (booking == null || booking.Id != id)
                return BadRequest("Invalid booking data");

            var existingBookingResult = await _bookingService.GetBookingByIdAsync(id);
            if (!existingBookingResult.IsSuccess)
                return NotFound(existingBookingResult.Error?.Message);

            var result = await _bookingService.UpdateBookingAsync(booking);

            if (!result.IsSuccess)
                return BadRequest(result.Error?.Message);

            return NoContent();
        }

        // DELETE: api/Booking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var deleteDto = new DeleteBookingDto { Id = id };
            var result = await _bookingService.DeleteBookingAsync(deleteDto);

            if (!result.IsSuccess)
                return BadRequest(result.Error?.Message);

            return NoContent();
        }

        // GET: api/Booking/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(Guid userId)
        {
            var result = await _bookingService.GetBookingsByUserIdAsync(userId);
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return NotFound("No bookings found for this user");

            return Ok(result.Value);
        }

        // GET: api/Booking/sportfield/{sportFieldId}
        [HttpGet("sportfield/{sportFieldId}")]
        public async Task<IActionResult> GetBookingsBySportFieldId(Guid sportFieldId)
        {
            var result = await _bookingService.GetBookingsBySportFieldIdAsync(sportFieldId);

            // Null va bo'sh tekshiruvi
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return NotFound("No bookings found for this sport field");

            return Ok(result.Value);
        }


        // GET: api/Booking
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetBookingsByUserIdAsync(Guid.Empty); // or any userId
            if (!result.IsSuccess || result.Value == null || !result.Value.Any())
                return NotFound("No bookings found");

            return Ok(result.Value);
        }
    }
}
