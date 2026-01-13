
namespace MiniDevTo.Messaging.Contracts
{
    public class ArticleApprovedEvent
    {
        public Guid ArticleId { get; set; }
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorEmail { get; set; } = string.Empty;
        public DateTime ApprovedAt { get; set; }
    }
}
