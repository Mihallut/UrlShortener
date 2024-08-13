namespace UrlShortener.Domain.Interfaces.Services
{
    public interface IUrlShorteningService
    {
        Task<string> GenerateUniqueCode();
    }
}
