namespace UrlShortener.Domain.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(Domain.Entities.User user);
        Task<Domain.Entities.User> GetByEmail(string email);
        Task<Domain.Entities.Role> GetRoleByName(string roleName);
    }
}
