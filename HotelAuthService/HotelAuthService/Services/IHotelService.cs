using HotelAuthService.DTOs;

namespace HotelAuthService.Services
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
        Task<IEnumerable<HotelDto>> SearchHotelsAsync(string location);
        Task<HotelDto> GetHotelByIdAsync(int hotelId);
        Task<IEnumerable<RoomDto>> GetRoomsByHotelIdAsync(int hotelId);
        Task<RoomDto> GetRoomByIdAsync(int roomId);

        Task<IEnumerable<HotelDto>> SearchHotelsAsync(
    string? location,
    decimal? minPrice,
    decimal? maxPrice,
    string? amenity);
    }
}