using MiniDevTo.Domain.Entities;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Author.Signup
{
    public class Author:Endpoint<SignupRequest, SignupResponse>
    {
//construtor
        private readonly AppDbContext _db;
        public Author(AppDbContext db)
        {
           _db = db;
        }
//end construtor
        public override void Configure()
        {
            Post("/author/signup");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SignupRequest req, CancellationToken ct)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = req.Email,
                Password = req.Password,
                Role = "Author"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync(ct);
            // Lógica de cadastro aqui
            await SendAsync(new SignupResponse()
            { Message = "Author created!" });
        }

    }
}


public class SignupRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignupResponse
{
    public string Message { get; set; }
}