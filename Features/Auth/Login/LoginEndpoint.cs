using Microsoft.EntityFrameworkCore;
using MiniDevTo.Infrastructure.Database;
using MiniDevTo.Services.Auth;

namespace MiniDevTo.Features.Auth.Login
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
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == rq.Email && u.Password == rq.Password, ct);

            if(user is null)
            {
                await SendAsync(new LoginResponse {
                    Message = "Credenciais inválidas.",
                    Token = "" }, StatusCodes.Status401Unauthorized);
                return;
            }

            var token= _tokenService.GenerateToken(user);

          

            await SendAsync(new LoginResponse { 

            Message="Login efectuado com sucesso",
            Token = token,
            Role = user.Role

            });
        }
    }
}
