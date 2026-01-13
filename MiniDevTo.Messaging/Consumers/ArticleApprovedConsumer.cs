using MassTransit;
using MiniDevTo.Messaging.Contracts;
using MiniDevTo.Messaging.Services;

namespace MiniDevTo.Messaging.Consumers
{
    public class ArticleApprovedConsumer : IConsumer<ArticleApprovedEvent>
    {
        private readonly IEmailService _emailService;

        public ArticleApprovedConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<ArticleApprovedEvent> context)
        {
            var msg = context.Message;
            Console.WriteLine($"[NOTIFICATION] Article approved: {msg.Title} by {msg.AuthorEmail}");
            //send email here

            string subject = "Seu artigo foi aprovado!";
            string body = $"<h1>Parabéns!</h1><p>Seu artigo <strong>{msg.Title}</strong> foi aprovado e já está disponível no site.</p>";

            await _emailService.SendEmailAsync(msg.AuthorEmail, subject, body);
            return ;
        }
    }
}
