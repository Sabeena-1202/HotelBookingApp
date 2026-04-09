using HotelAuthService.DTOs;
using HotelAuthService.Repositories;

namespace HotelAuthService.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();

            return hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Description = h.Description,
                Amenities = h.Amenities,
                Rating = h.Rating,
                Rooms = h.Rooms.Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type,
                    Description = r.Description,
                    PricePerNight = r.PricePerNight,
                    MaxGuests = r.MaxGuests,
                    IsAvailable = r.IsAvailable,
                    HotelId = r.HotelId
                }).ToList()
            });
        }

        public async Task<IEnumerable<HotelDto>> SearchHotelsAsync(
            string location)
        {
            var hotels = await _hotelRepository
                .SearchHotelsAsync(location);

            return hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Description = h.Description,
                Amenities = h.Amenities,
                Rating = h.Rating,
                Rooms = h.Rooms.Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type,
                    Description = r.Description,
                    PricePerNight = r.PricePerNight,
                    MaxGuests = r.MaxGuests,
                    IsAvailable = r.IsAvailable,
                    HotelId = r.HotelId
                }).ToList()
            });
        }

        public async Task<HotelDto> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository
                .GetHotelByIdAsync(hotelId);

            if (hotel == null)
            {
                throw new Exception("Hotel not found");
            }

            return new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Location = hotel.Location,
                Description = hotel.Description,
                Amenities = hotel.Amenities,
                Rating = hotel.Rating,
                Rooms = hotel.Rooms.Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type,
                    Description = r.Description,
                    PricePerNight = r.PricePerNight,
                    MaxGuests = r.MaxGuests,
                    IsAvailable = r.IsAvailable,
                    HotelId = r.HotelId
                }).ToList()
            };
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByHotelIdAsync(
            int hotelId)
        {
            var rooms = await _hotelRepository
                .GetRoomsByHotelIdAsync(hotelId);

            return rooms.Select(r => new RoomDto
            {
                RoomId = r.RoomId,
                RoomNumber = r.RoomNumber,
                Type = r.Type,
                Description = r.Description,
                PricePerNight = r.PricePerNight,
                MaxGuests = r.MaxGuests,
                IsAvailable = r.IsAvailable,
                HotelId = r.HotelId
            });
        }

        public async Task<RoomDto> GetRoomByIdAsync(int roomId)
        {
            var room = await _hotelRepository
                .GetRoomByIdAsync(roomId);

            if (room == null)
            {
                throw new Exception("Room not found");
            }

            return new RoomDto
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                Description = room.Description,
                PricePerNight = room.PricePerNight,
                MaxGuests = room.MaxGuests,
                IsAvailable = room.IsAvailable,
                HotelId = room.HotelId
            };
        }

        public async Task<IEnumerable<HotelDto>> SearchHotelsAsync(
    string? location,
    decimal? minPrice,
    decimal? maxPrice,
    string? amenity)
        {
            var hotels = await _hotelRepository
                .SearchHotelsAsync(location, minPrice, maxPrice, amenity);

            return hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Description = h.Description,
                Amenities = h.Amenities,
                Rating = h.Rating,
                Rooms = h.Rooms.Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type,
                    Description = r.Description,
                    PricePerNight = r.PricePerNight,
                    MaxGuests = r.MaxGuests,
                    IsAvailable = r.IsAvailable,
                    HotelId = r.HotelId
                }).ToList()
            });
        }
    }
}