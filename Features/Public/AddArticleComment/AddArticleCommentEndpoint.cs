using MiniDevTo.Domain.Entities;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Public.AddArticleComment
{
    public class AddArticleCommentEndpoint:Endpoint<AddArticleCommentRequest, AddArticleCommentResponse>
    {
        private readonly AppDbContext _db;

        public AddArticleCommentEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Post("/articles/{id}/comments");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddArticleCommentRequest req,CancellationToken ct)
        {
            var article = await _db.Articles.FindAsync(Guid.Parse(Route<string>("id")));
            
            if(article is null || article.Status != ArticleStatus.Approved)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                ArticleId = article.Id,
                Content = req.Content,
                AuthorName = req.AuthorName,
                CreatedAt = DateTime.UtcNow
            };

            _db.Comments.Add(comment);
            await _db.SaveChangesAsync(ct);

            await SendAsync(new AddArticleCommentResponse { Message = "Comment added!" });
        }
    }
}

public class AddArticleCommentRequest
{
    public required string AuthorName { get; set; }
    public required string Content { get; set; }
}

public class AddArticleCommentResponse
{
    public string Message { get; set; } = string.Empty;
}