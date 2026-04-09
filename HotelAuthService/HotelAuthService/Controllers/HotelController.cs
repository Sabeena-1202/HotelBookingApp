using HotelAuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelAuthService.Controllers
{
    [ApiController]
    [Route("api/hotel")]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(
            IHotelService hotelService,
            ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllHotels()
        {
            _logger.LogInformation("Fetching all hotels");

            var hotels = await _hotelService.GetAllHotelsAsync();

            _logger.LogInformation(
                "Fetched {Count} hotels successfully",
                hotels.Count());

            return Ok(hotels);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchHotels(
            [FromQuery] string? location,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? amenity)
        {
            _logger.LogInformation(
                "Searching hotels - Location: {Location}, MinPrice: {Min}, MaxPrice: {Max}, Amenity: {Amenity}",
                location, minPrice, maxPrice, amenity);

            var hotels = await _hotelService
                .SearchHotelsAsync(location, minPrice, maxPrice, amenity);

            _logger.LogInformation(
                "Found {Count} hotels matching filters",
                hotels.Count());

            return Ok(hotels);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            _logger.LogInformation(
                "Fetching hotel with id: {HotelId}", hotelId);

            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);

            _logger.LogInformation(
                "Fetched hotel: {HotelName} successfully", hotel.Name);

            return Ok(hotel);
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
        {
            _logger.LogInformation(
                "Fetching rooms for hotel id: {HotelId}", hotelId);

            var rooms = await _hotelService
                .GetRoomsByHotelIdAsync(hotelId);

            _logger.LogInformation(
                "Fetched {Count} rooms for hotel id: {HotelId}",
                rooms.Count(), hotelId);

            return Ok(rooms);
        }

        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetRoomById(int roomId)
        {
            _logger.LogInformation(
                "Fetching room with id: {RoomId}", roomId);

            var room = await _hotelService.GetRoomByIdAsync(roomId);

            _logger.LogInformation(
                "Fetched room: {RoomType} successfully", room.Type);

            return Ok(room);
        }

        [HttpGet("validate-token")]
        public IActionResult ValidateToken()
        {
            _logger.LogInformation("Token validation requested");
            return Ok(new { message = "Token is valid" });
        }
    }
}