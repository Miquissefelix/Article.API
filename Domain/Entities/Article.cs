namespace MiniDevTo.Domain.Entities
{
    public class Article
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ArticleStatus Status { get; set; } = ArticleStatus.Pending;
        public string? RejectionReason { get; set; }

        // Relacionamentos
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = default!;

        public List<Comment> Comments { get; set; } = new();
    }
}

public enum ArticleStatus
{
    Pending,
    Approved,
    Rejected
}