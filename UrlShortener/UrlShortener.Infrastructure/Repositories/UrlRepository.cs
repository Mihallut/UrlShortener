using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Infrastructure.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApiDbContext _context;
        public UrlRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task AddUrl(UrlInfo urlInfo)
        {
            await _context.UrlInfos.AddAsync(urlInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<UrlInfo> GetByCode(string code)
        {
            var codeDB = await _context.UrlInfos.FirstOrDefaultAsync(ul => ul.Code == code);
            return codeDB;
        }

        public Task<UrlInfo> DeleteUrl(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UrlInfo>> GetAll()
        {
            return await _context.UrlInfos.Include(ul => ul.CreatedBy).ToListAsync();
        }
    }
}
