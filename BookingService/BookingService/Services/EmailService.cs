using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace BookingService.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendBookingConfirmationAsync(string toEmail, string guestName, int bookingId, DateTime checkIn, DateTime checkOut)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _config["EmailSettings:SenderName"],
                _config["EmailSettings:SenderEmail"]
            ));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = $"Booking Confirmed — #{bookingId}";

            email.Body = new TextPart("html")
            {
                Text = $@"
                <h2>Hello {guestName},</h2>
                <p>Your booking has been <strong>confirmed</strong>!</p>
                <p><strong>Booking ID:</strong> {bookingId}</p>
                <p><strong>Check-In:</strong> {checkIn:dd MMM yyyy}</p>
                <p><strong>Check-Out:</strong> {checkOut:dd MMM yyyy}</p>
                <br/>
                <p>Thank you for choosing Hotel Booking App!</p>"
            };

            await SendEmailAsync(email);
        }

        public async Task SendCancellationEmailAsync(string toEmail, string guestName, int bookingId)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _config["EmailSettings:SenderName"],
                _config["EmailSettings:SenderEmail"]
            ));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = $"Booking Cancelled — #{bookingId}";

            email.Body = new TextPart("html")
            {
                Text = $@"
                <h2>Hello {guestName},</h2>
                <p>Your booking <strong>#{bookingId}</strong> has been <strong>cancelled</strong>.</p>
                <p>If you have any questions, please contact us.</p>
                <br/>
                <p>Hotel Booking App Team</p>"
            };

            await SendEmailAsync(email);
        }

        private async Task SendEmailAsync(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _config["EmailSettings:Host"],
                int.Parse(_config["EmailSettings:Port"]!),
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(
                _config["EmailSettings:SenderEmail"],
                _config["EmailSettings:Password"]
            );
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}