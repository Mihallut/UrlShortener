using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApiDbContext _context;
        public UsersRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            return role;
        }
    }
}
