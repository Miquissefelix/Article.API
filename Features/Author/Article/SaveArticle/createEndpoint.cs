using MiniDevTo.Domain.Entities;
using MiniDevTo.Infrastructure.Database;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace MiniDevTo.Features.Author.Article.SaveArticle
{
    public class createEndpoint:Endpoint<CreateArticleRequest,CreateArticleResponse>
    {
        private readonly AppDbContext _db;
        public createEndpoint(AppDbContext db)
        {
            _db = db;
        }


        public override void Configure()
        {
            Post("/author/article/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateArticleRequest req, CancellationToken ct)
        {
            var authorExists = await _db.Users.AnyAsync(u => u.Id == req.AuthorId && u.Role == "Author");

            if (!authorExists)
            {
                await SendAsync(new CreateArticleResponse { Message = "Author not found." }, 404);
                return;
            }

            //var article = new MiniDevTo.Domain.Entities.Article
            //{
            //    Id = Guid.NewGuid(),
            //    Title = req.Title,
            //    Content = req.Content,
            //    AuthorId = req.AuthorId,
            //    Status = ArticleStatus.Pending

            //};

            //_db.Articles.Add(article);
            await _db.SaveChangesAsync(ct);

            await SendAsync(new CreateArticleResponse
            {
                Message = "Article created successfully.",
                //ArticleId = article.Id
            });

        }



        

    }
}

public class CreateArticleRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid AuthorId { get; set; } // Recebido do frontend
}

public class CreateArticleResponse
{
    public string Message { get; set; } = string.Empty;
    public Guid ArticleId { get; set; }
}
