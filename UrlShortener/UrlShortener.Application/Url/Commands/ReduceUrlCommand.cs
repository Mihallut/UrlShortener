using MediatR;
using System.Text.Json.Serialization;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.Application.Url.Commands
{
    public class ReduceUrlCommand : IRequest<UrlInfoDto>
    {
        public string Original { get; set; }
        [JsonIgnore]
        public string ContextRequestScheme { get; set; }
        [JsonIgnore]
        public string ContextRequestHost { get; set; }
    }
}
