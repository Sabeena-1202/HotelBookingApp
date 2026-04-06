namespace BookingService.Services
{
    public interface IEmailService
    {
        Task SendBookingConfirmationAsync(string toEmail, string guestName, int bookingId, DateTime checkIn, DateTime checkOut);
        Task SendCancellationEmailAsync(string toEmail, string guestName, int bookingId);
    }
}