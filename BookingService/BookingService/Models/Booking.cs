using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [MaxLength(200)]
        public string? SpecialRequests { get; set; }

        [Required]
        [MaxLength(100)]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string GuestName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}