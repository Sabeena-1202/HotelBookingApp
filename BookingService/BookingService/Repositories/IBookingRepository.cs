using BookingService.Models;

namespace BookingService.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking?> UpdateBookingStatusAsync(int id, string status);
        Task<bool> DeleteBookingAsync(int id);
    }
}