using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Public.GetArticleComments;

public class GetArticleCommentsEndpoint : Endpoint<GetArticleCommentsRequest, List<GetArticleCommentsResponse>>
{
    private readonly AppDbContext _db;

    public GetArticleCommentsEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/articles/{id}/comments");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetArticleCommentsRequest req, CancellationToken ct)
    {
        var articleExists = await _db.Articles
            .AnyAsync(a => a.Id == req.ArticleId && a.Status == ArticleStatus.Approved, ct);

        if (!articleExists)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var comments = await _db.Comments
            .Where(c => c.ArticleId == req.ArticleId)
            .OrderByDescending(c => c.CreatedAt)
            .Take(50)
            .Select(c => new GetArticleCommentsResponse
            {
                AuthorName = c.AuthorName,
                Content = c.Content,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync(ct);

        await SendAsync(comments);
    }
}

public class GetArticleCommentsRequest
{
    [FromRoute] public Guid ArticleId { get; set; }
}

public class GetArticleCommentsResponse
{
    public string AuthorName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
