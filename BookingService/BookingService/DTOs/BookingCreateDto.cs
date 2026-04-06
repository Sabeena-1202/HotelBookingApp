using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.DTOs
{
    public class BookingCreateDto
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Guests must be between 1 and 10")]
        public int NumberOfGuests { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [MaxLength(200)]
        public string? SpecialRequests { get; set; }

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string GuestName { get; set; } = string.Empty;
    }
}