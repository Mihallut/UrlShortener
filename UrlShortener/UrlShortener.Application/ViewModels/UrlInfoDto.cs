using UrlShortener.Application.Mappings;

namespace UrlShortener.Application.ViewModels
{
    public class UrlInfoDto : IMapFrom<Domain.Entities.UrlInfo>
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortedUrl { get; set; }
        public UserDto CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
