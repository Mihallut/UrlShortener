using MediatR;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.Application.Url.Queries
{
    public class GetAllQuery : IRequest<List<UrlInfoDto>>
    {
    }
}
