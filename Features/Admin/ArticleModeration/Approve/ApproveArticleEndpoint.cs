using MassTransit;
using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;
using MiniDevTo.Messaging.Contracts;


namespace MiniDevTo.Features.Admin.ArticleModeration.Approve
{
    public class ApproveArticleEndpoint : Endpoint<ApproveArticleRequest, ApproveResponse>
    {
        private readonly AppDbContext _db;
        private readonly IPublishEndpoint _publish;
            public ApproveArticleEndpoint (AppDbContext db, IPublishEndpoint publish)
        {
            _db = db;
            _publish = publish;
        }

        public override void Configure()
        {
            Post("/admin/articles/approve/{ArticleId}");
            AuthSchemes("Bearer");
            Roles("Admin");

        }
        public override async Task HandleAsync(ApproveArticleRequest req, CancellationToken ct)
        {
            var article = await _db.Articles.Include(a => a.Author)
                .FirstOrDefaultAsync(a => a.Id == req.ArticleId,ct);
                //FindAsync(req.ArticleId);

            if (article is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            article.Status = ArticleStatus.Approved;
           
            await _db.SaveChangesAsync(ct);
            await SendAsync(new ApproveResponse { Message = "Article Approved" });

            await _publish.Publish(new ArticleApprovedEvent
            {
                ArticleId=article.Id,
                Title=article.Title,
                AuthorEmail=article.Author.Email,
                ApprovedAt=DateTime.UtcNow,
            }, ct);

        }
    }
}

public class ApproveArticleRequest
{
    public Guid ArticleId { get; set; }
}

public class ApproveResponse
{
    public string Message { get; set; } = "";
}