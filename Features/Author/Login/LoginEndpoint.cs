using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;
using MiniDevTo.Services.Auth;

namespace MiniDevTo.Features.Author.Login
{
    public class LoginEndpoint: Endpoint<LoginRequest, LoginResponse>
    {
      
        private readonly AppDbContext _db;
        private readonly ITokenService _tokenService;
        public LoginEndpoint(AppDbContext db, ITokenService tokenService)
        {
            _db = db;   
            _tokenService = tokenService;
        }

        public override void Configure()
        {
            Post("/auth/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest rq,CancellationToken ct)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == rq.Email, ct);

            if(user is null || user.Password != rq.Password)
            {
                await SendAsync(new LoginResponse {
                    Message = "Credenciais inválidas.",
                    Token = "" }, StatusCodes.Status401Unauthorized);
                return;
            }

            var token= _tokenService.GenerateToken(user);

          

            await SendAsync(new LoginResponse { 

            Message="Login efectuado com sucesso",
            Token = token

            });
        }
    }
}
