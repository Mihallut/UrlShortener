using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces.Repositories
{
    public interface IUrlRepository
    {
        Task<List<UrlInfo>> GetAll();
        Task<UrlInfo> DeleteUrl(Guid id);
        Task AddUrl(UrlInfo urlInfo);
        Task<UrlInfo> GetByCode(string code);
    }
}
