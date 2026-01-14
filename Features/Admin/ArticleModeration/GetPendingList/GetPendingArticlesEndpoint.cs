using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Admin.ArticleModeration.GetPendingList
{
    public class GetPendingArticlesEndpoint : EndpointWithoutRequest<List<PendingArticleResponse>>
    {
        private readonly AppDbContext _db;

        public GetPendingArticlesEndpoint(AppDbContext db) {
            _db = db;
        }

        public override void Configure()
        {
            Get("/admin/articles/pending");
            AllowAnonymous();
            //Roles("Admin");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var articles = await _db.Articles
           .Where(a => a.Status == ArticleStatus.Pending)
           .Select(a => new PendingArticleResponse
           {
               Id = a.Id,
               Title = a.Title,
               Content=a.Content,
               AuthorId = a.AuthorId,
               SubmittedAt = a.CreatedAt
           })
           .ToListAsync(ct);

            await SendAsync(articles, cancellation: ct);
        }
    }
}

public class PendingArticleResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
    public DateTime SubmittedAt { get; set; }
}