using Microsoft.EntityFrameworkCore;
using MiniDevTo.Features.Author.Login;
using MiniDevTo.Infrastructure.Database;

namespace MiniDevTo.Features.Admin.Login
{
    public class AdminLoginEndpoint:Endpoint<LoginRequest,LoginResponse>
    {
        private readonly AppDbContext _db;

        public AdminLoginEndpoint(AppDbContext db) { 
        
            _db = db;
        }

        public override void Configure()
        {
            Post("/admin/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var user= await _db.Users.FirstOrDefaultAsync(u=>u.Email==req.Email &&
            u.Password==req.Password && u.Role=="Admin");

            if (user is null)
            {
                await SendAsync(new LoginResponse { Message = "Invalid credentials." }, 401);
                return;
            }

            await SendAsync(new LoginResponse { Message = "Admin logged in successfully." });
        } 

    }
}




