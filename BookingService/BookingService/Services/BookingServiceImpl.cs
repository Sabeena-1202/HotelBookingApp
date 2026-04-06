using BookingService.DTOs;
using BookingService.Models;
using BookingService.Repositories;

namespace BookingService.Services
{
    public class BookingServiceImpl : IBookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IEmailService _emailService;

        public BookingServiceImpl(IBookingRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<BookingResponseDto> CreateBookingAsync(BookingCreateDto dto, int userId)
        {
            var booking = new Booking
            {
                UserId = userId,
                HotelId = dto.HotelId,
                RoomId = dto.RoomId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                NumberOfGuests = dto.NumberOfGuests,
                TotalPrice = dto.TotalPrice,
                SpecialRequests = dto.SpecialRequests,
                GuestEmail = dto.GuestEmail,
                GuestName = dto.GuestName,
                Status = "Confirmed",
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateBookingAsync(booking);

            // Send confirmation email
            await _emailService.SendBookingConfirmationAsync(
                created.GuestEmail,
                created.GuestName,
                created.Id,
                created.CheckInDate,
                created.CheckOutDate
            );

            return MapToResponseDto(created);
        }

        public async Task<BookingResponseDto?> GetBookingByIdAsync(int id)
        {
            var booking = await _repository.GetBookingByIdAsync(id);
            return booking == null ? null : MapToResponseDto(booking);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetBookingsByUserIdAsync(int userId)
        {
            var bookings = await _repository.GetBookingsByUserIdAsync(userId);
            return bookings.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
        {
            var bookings = await _repository.GetAllBookingsAsync();
            return bookings.Select(MapToResponseDto);
        }

        public async Task<BookingResponseDto?> CancelBookingAsync(int id, int userId)
        {
            var booking = await _repository.GetBookingByIdAsync(id);
            if (booking == null || booking.UserId != userId) return null;

            var updated = await _repository.UpdateBookingStatusAsync(id, "Cancelled");
            if (updated == null) return null;

            // Send cancellation email
            await _emailService.SendCancellationEmailAsync(
                updated.GuestEmail,
                updated.GuestName,
                updated.Id
            );

            return MapToResponseDto(updated);
        }

        private static BookingResponseDto MapToResponseDto(Booking booking)
        {
            return new BookingResponseDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                HotelId = booking.HotelId,
                RoomId = booking.RoomId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfGuests = booking.NumberOfGuests,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status,
                SpecialRequests = booking.SpecialRequests,
                GuestEmail = booking.GuestEmail,
                GuestName = booking.GuestName,
                CreatedAt = booking.CreatedAt
            };
        }
    }
}