using MiniDevTo.Domain.Entities;

namespace MiniDevTo.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
