using HotelAuthService.Models;

namespace HotelAuthService.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<IEnumerable<Hotel>> SearchHotelsAsync(string location);
        Task<Hotel?> GetHotelByIdAsync(int hotelId);
        Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Room?> GetRoomByIdAsync(int roomId);

        Task<IEnumerable<Hotel>> SearchHotelsAsync(
    string? location,
    decimal? minPrice,
    decimal? maxPrice,
    string? amenity);
    }
}