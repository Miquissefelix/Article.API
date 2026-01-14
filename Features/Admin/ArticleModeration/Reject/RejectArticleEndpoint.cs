using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Admin.ArticleModeration.Reject
{
    public class RejectArticleEndpoint:Endpoint<RejectArticleRequest>
    {
        private readonly AppDbContext _db;
        public RejectArticleEndpoint(AppDbContext db)
        { 
        _db = db;
        }

        public override void Configure()
        {
            Post("/admin/articles/reject/{ArticleId}");
            AuthSchemes("Bearer");
            Roles("Admin");
        }

        public override async Task HandleAsync(RejectArticleRequest req, CancellationToken ct)
        {
            var article = await _db.Articles.FindAsync(req.ArticleId);

            if (article is null)
            {
                await SendNotFoundAsync();
                return;
            }

            article.Status = ArticleStatus.Rejected;
            article.RejectionReason = req.Reason;
            await _db.SaveChangesAsync(ct);

            await SendOkAsync();
        }
    }
}

public class RejectArticleRequest
{
    public Guid ArticleId { get; set; }
    public required string Reason { get; set; }
}