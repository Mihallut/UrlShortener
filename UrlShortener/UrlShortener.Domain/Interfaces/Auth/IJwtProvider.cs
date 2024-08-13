using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
