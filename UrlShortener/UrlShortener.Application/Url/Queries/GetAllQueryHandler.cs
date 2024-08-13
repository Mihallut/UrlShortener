using AutoMapper;
using MediatR;
using UrlShortener.Application.ViewModels;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Application.Url.Queries
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, List<UrlInfoDto>>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }
        public async Task<List<UrlInfoDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var urlInfos = await _urlRepository.GetAll();
            var urlInfosDto = _mapper.Map<List<UrlInfoDto>>(urlInfos);
            return urlInfosDto;
        }
    }
}
