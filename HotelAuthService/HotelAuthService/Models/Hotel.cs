namespace HotelAuthService.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Amenities { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}