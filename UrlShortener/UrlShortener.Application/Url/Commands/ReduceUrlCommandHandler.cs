using AutoMapper;
using MediatR;
using UrlShortener.Application.ViewModels;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Interfaces.Services;

namespace UrlShortener.Application.Url.Commands
{
    public class ReduceUrlCommandHandler : IRequestHandler<ReduceUrlCommand, UrlInfoDto>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUrlShorteningService _shorteningService;
        private readonly IMapper _mapper;

        public ReduceUrlCommandHandler(IUrlRepository urlRepository, IUrlShorteningService shorteningService, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _shorteningService = shorteningService;
            _mapper = mapper;
        }
        public async Task<UrlInfoDto> Handle(ReduceUrlCommand command, CancellationToken cancellationToken)
        {
            var code = await _shorteningService.GenerateUniqueCode();
            var urlInfo = new Domain.Entities.UrlInfo
            {
                Id = Guid.NewGuid(),
                OriginalUrl = command.Original,
                CreationDate = DateTime.UtcNow,
                Code = code,
                ShortedUrl = $"{command.ContextRequestScheme}://{command.ContextRequestHost}/{code}",
                IdCreatedBy = new Guid("e1427c7f-7be3-4f0a-bd04-c32baaca76fa")
            };

            await _urlRepository.AddUrl(urlInfo);

            var urlInfoDto = _mapper.Map<UrlInfoDto>(urlInfo);
            return urlInfoDto;
        }
    }
}
