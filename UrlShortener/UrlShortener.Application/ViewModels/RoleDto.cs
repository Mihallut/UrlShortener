using UrlShortener.Application.Mappings;

namespace UrlShortener.Application.ViewModels
{
    public class RoleDto : IMapFrom<Domain.Entities.Role>
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
