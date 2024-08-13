using UrlShortener.Application.Mappings;

namespace UrlShortener.Application.ViewModels
{
    public class UserDto : IMapFrom<Domain.Entities.User>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public RoleDto Role { get; set; }
    }
}
