using Microsoft.IdentityModel.Tokens;
using MiniDevTo.Domain.Entities;
using MiniDevTo.Infrastructure.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniDevTo.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;
        public TokenService(JwtOptions options) { 
        
        _options = options;
        }

        public string GenerateToken(User user) 
        {
            var claims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
             new Claim(ClaimTypes.Email, user.Email),
             new Claim(ClaimTypes.Role, user.Role)
          };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
           issuer: _options.Issuer,
           audience: _options.Audience,
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
           signingCredentials: creds
       );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
