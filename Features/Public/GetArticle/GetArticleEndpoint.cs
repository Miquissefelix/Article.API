using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Public.GetArticle;

public class GetArticleEndpoint : Endpoint<GetArticleRequest, GetArticleResponse>
{
    private readonly AppDbContext _db;

    public GetArticleEndpoint(AppDbContext db)
    {
        _db = db;
    }
   

    public override void Configure()
    {
        Get("/articles/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetArticleRequest req, CancellationToken ct)
    {
        var article = await _db.Articles
            .Where(a => a.Id == req.Id && a.Status == ArticleStatus.Approved)
            .FirstOrDefaultAsync(ct);

        if (article is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(new GetArticleResponse
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            CreatedAt = article.CreatedAt
        });
    }
}

public class GetArticleRequest
{
    [FromRoute] public Guid Id { get; set; }
}

public class GetArticleResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
