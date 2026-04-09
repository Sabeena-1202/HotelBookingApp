using BookingService.DTOs;

namespace BookingService.Services
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingCreateDto dto, int userId);
        Task<BookingResponseDto?> GetBookingByIdAsync(int id);
        Task<IEnumerable<BookingResponseDto>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync();
        Task<BookingResponseDto?> CancelBookingAsync(int id, int userId);
    }
}