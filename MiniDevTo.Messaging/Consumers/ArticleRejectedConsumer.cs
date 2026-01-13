using MassTransit;
using MiniDevTo.Messaging.Contracts;
using MiniDevTo.Messaging.Services;


namespace MiniDevTo.Messaging.Consumers;
 public class ArticleRejectedConsumer : IConsumer<ArticleRejectedEvent>
{
    private readonly IEmailService _emailService;

    public ArticleRejectedConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async  Task Consume(ConsumeContext<ArticleRejectedEvent> context)
    {
        var msg = context.Message;
        // Envia e-mail usando SmtpClient
        string subject = "Seu artigo foi rejeitado";
        string body = $"<h1>Seu artigo foi rejeitado</h1><p>O artigo" +
            $" <strong>{msg.Title}</strong> foi rejeitado com o seguinte motivo:" +
            $" {msg.Reason}</p>";
        await _emailService.SendEmailAsync(msg.AuthorEmail, subject, body);
        return ;
    }

}

