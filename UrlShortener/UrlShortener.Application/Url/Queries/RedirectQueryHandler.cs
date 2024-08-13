using AutoMapper;
using MediatR;
using UrlShortener.Application.ViewModels;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Application.Url.Queries
{
    public class RedirectQueryHandler : IRequestHandler<RedirectQuery, UrlInfoDto>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public RedirectQueryHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }
        public async Task<UrlInfoDto> Handle(RedirectQuery request, CancellationToken cancellationToken)
        {
            var urlInfo = await _urlRepository.GetByCode(request.Code);
            if (urlInfo == null) { return null; }
            var urlInfoDto = _mapper.Map<UrlInfoDto>(urlInfo);
            return urlInfoDto;
        }
    }
}
