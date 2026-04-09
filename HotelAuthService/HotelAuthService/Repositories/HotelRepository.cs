using HotelAuthService.Data;
using HotelAuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelAuthService.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;

        public HotelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> SearchHotelsAsync(string location)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .Where(h => h.Location.ToLower()
                .Contains(location.ToLower()))
                .ToListAsync();
        }

        public async Task<Hotel?> GetHotelByIdAsync(int hotelId)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.HotelId == hotelId);
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return await _context.Rooms
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int roomId)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.RoomId == roomId);
        }

        public async Task<IEnumerable<Hotel>> SearchHotelsAsync(
    string? location,
    decimal? minPrice,
    decimal? maxPrice,
    string? amenity)
        {
            var query = _context.Hotels
                .Include(h => h.Rooms)
                .AsQueryable();

            if (!string.IsNullOrEmpty(location))
                query = query.Where(h =>
                    h.Location.ToLower().Contains(location.ToLower()));

            if (!string.IsNullOrEmpty(amenity))
                query = query.Where(h =>
                    h.Amenities.ToLower().Contains(amenity.ToLower()));

            if (minPrice.HasValue)
                query = query.Where(h =>
                    h.Rooms.Any(r => r.PricePerNight >= minPrice.Value));

            if (maxPrice.HasValue)
                query = query.Where(h =>
                    h.Rooms.Any(r => r.PricePerNight <= maxPrice.Value));

            return await query.ToListAsync();
        }
    }
}