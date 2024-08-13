using MediatR;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.Application.Url.Queries
{
    public class RedirectQuery : IRequest<UrlInfoDto>
    {
        public string Code { get; set; }
    }
}
