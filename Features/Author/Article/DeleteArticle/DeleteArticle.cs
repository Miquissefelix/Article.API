using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Author.Article.DeleteArticle
{
    public class DeleteArticle:Endpoint<DeleteArticleRequest>
    {
        private readonly AppDbContext _db;

        public DeleteArticle(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Delete("/author/articles/{id}");
            AllowAnonymous();//futuramente trocar por Roles
        }

        public override async Task HandleAsync(DeleteArticleRequest req,CancellationToken ct) {

            var article = await _db.Articles.FirstOrDefaultAsync(
                a => a.Id == req.Id && a.AuthorId == req.AuthorId,ct);

            if (article is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _db.Articles.Remove(article);
            await _db.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }
    }
}

public class DeleteArticleRequest
{
    public Guid Id { get; set; }
    public required Guid AuthorId { get; set; }
}

