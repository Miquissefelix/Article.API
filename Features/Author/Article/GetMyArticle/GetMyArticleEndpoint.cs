using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Author.Article.GetMyArticle
{
    public class GetMyArticleEndpoint:Endpoint<GetMyArticleRequest, List<GetMyArticleResponse>>
    {
        private readonly AppDbContext _db;

        public GetMyArticleEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/author/articles/mine");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetMyArticleRequest req, CancellationToken ct)
        {
            var articles = await _db.Articles
                .Where(a => a.AuthorId == req.AuthorId)
                .Select(a => new GetMyArticleResponse
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    Status = a.Status.ToString(),
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync(ct);

            await SendAsync(articles);
        }
    }
}


public class GetMyArticleRequest
{
    public Guid AuthorId { get; set; }
}

public class GetMyArticleResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}