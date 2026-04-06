using BookingService.DTOs;
using BookingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        // POST api/booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized(new { message = "Invalid token" });

            _logger.LogInformation("User {UserId} creating booking for Hotel {HotelId}", userId, dto.HotelId);

            var result = await _bookingService.CreateBookingAsync(dto, userId);

            return CreatedAtAction(nameof(GetBookingById), new { id = result.Id }, new
            {
                success = true,
                message = "Booking created successfully",
                data = result
            });
        }

        // GET api/booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var userId = GetUserIdFromToken();

            _logger.LogInformation("User {UserId} fetching booking {BookingId}", userId, id);

            var result = await _bookingService.GetBookingByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Booking not found" });

            return Ok(new { success = true, data = result });
        }

        // GET api/booking/my-bookings
        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized(new { message = "Invalid token" });

            _logger.LogInformation("User {UserId} fetching their bookings", userId);

            var result = await _bookingService.GetBookingsByUserIdAsync(userId);

            return Ok(new { success = true, data = result });
        }

        // GET api/booking/all  (admin only)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings()
        {
            _logger.LogInformation("Fetching all bookings");

            var result = await _bookingService.GetAllBookingsAsync();

            return Ok(new { success = true, data = result });
        }

        // PUT api/booking/{id}/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == 0)
                return Unauthorized(new { message = "Invalid token" });

            _logger.LogInformation("User {UserId} cancelling booking {BookingId}", userId, id);

            var result = await _bookingService.CancelBookingAsync(id, userId);
            if (result == null)
                return NotFound(new { message = "Booking not found or not authorized" });

            return Ok(new { success = true, message = "Booking cancelled successfully", data = result });
        }

        // Helper — extracts UserId from JWT token
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub")
                           ?? User.FindFirst("userId");

            if (userIdClaim == null) return 0;

            return int.TryParse(userIdClaim.Value, out var userId) ? userId : 0;
        }
    }
}