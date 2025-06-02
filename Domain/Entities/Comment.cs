namespace MiniDevTo.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento com o artigo
        public Guid ArticleId { get; set; }
        public Article Article { get; set; } = default!;

        //saber quem comentou
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public string AuthorName { get; internal set; }
    }
}
