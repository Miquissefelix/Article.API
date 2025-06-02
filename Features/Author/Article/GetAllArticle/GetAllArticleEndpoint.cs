using MiniDevTo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
namespace MiniDevTo.Features.Author.Article.GetAllArticle
{
    public class GetAllArticleEndpoint : EndpointWithoutRequest<List<GetAllArticleResponse>>
    {
        private readonly AppDbContext _db;
        public GetAllArticleEndpoint(AppDbContext db){
            _db = db;
         }


        public override void Configure()
        {
            Get("/author/articles");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var articles = await _db.Articles
                .Select(a => new GetAllArticleResponse
                {
                    Id = a.Id,
                    Title = a.Title,
                    AuthorEmail = a.Author.Email,
                    Status = a.Status.ToString(),
                    CreatedAt = a.CreatedAt
                })
            .ToListAsync(ct);
            await SendAsync(articles);

        }
    }
}

public class GetAllArticleResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}