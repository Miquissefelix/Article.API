using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Author.Article.EditArticle
{
    public class EditArticleEndpoint:Endpoint<EditArticleRequest,EditArticleResponse>
    {
        private readonly AppDbContext _db;
        public EditArticleEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Put("");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EditArticleRequest req, CancellationToken ct)
        {
            var article = await _db.Articles.FirstOrDefaultAsync(
                a => a.Id == req.Id && a.AuthorId == req.AuthorId);

            if(article is null)
            {
               await SendNotFoundAsync(ct);
                return;
            }

            // Atualiza o conteúdo e define status como Pending
            article.Title = req.Title;
            article.Content = req.Content;
            article.Status = ArticleStatus.Pending;
            article.UpdatedAt = DateTime.UtcNow;


            await _db.SaveChangesAsync(ct);

           await SendAsync(new EditArticleResponse
            {
                Message = "Article updated. Awaiting new approval."
            });
        }
    }
}

public class EditArticleRequest
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid AuthorId { get; set; }
}

public class EditArticleResponse
{
    public string Message { get; set; } = string.Empty;
}
