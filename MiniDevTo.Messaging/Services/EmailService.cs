using System.Net;
using System.Net.Mail;

namespace MiniDevTo.Messaging.Services
{
    public class EmailService: IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _from;

        public EmailService()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("fmiquissejose@gmail.com", "rndksfftaodnldpo"),
                EnableSsl = true
            };

            _from = "fmiquissejose@gmail.com";
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mail = new MailMessage(_from, to, subject, body)
            {
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(mail);

        }
        
    }
}
