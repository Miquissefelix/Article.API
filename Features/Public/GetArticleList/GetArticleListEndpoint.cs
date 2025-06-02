using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Public.GetArticleList;
public class GetArticleListEndpoint : EndpointWithoutRequest<List<GetArticleListResponse>>
{
    private readonly AppDbContext _db;

    public GetArticleListEndpoint(AppDbContext db) {

        _db = db;
    } 

    public override void Configure()
    {
        Get("/articles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var articles = await _db.Articles
            .Where(a => a.Status == ArticleStatus.Approved)
            .OrderByDescending(a => a.CreatedAt)
            .Take(50)
            .Select(a => new GetArticleListResponse
            {
                Id = a.Id,
                Title = a.Title,
                Summary = a.Content.Length > 100 ? a.Content.Substring(0,100) + "..." : a.Content,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync(ct);

        await SendAsync(articles);
    }
}

public class GetArticleListResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
