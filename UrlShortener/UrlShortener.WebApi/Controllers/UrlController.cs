using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Url.Commands;
using UrlShortener.Application.Url.Queries;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HttpContext _context;
        public UrlController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _context = httpContextAccessor.HttpContext;
        }

        [Authorize]
        [HttpPost("reduce-url")]
        public async Task<ActionResult<UrlInfoDto>> ReduceUrl([FromQuery] string url, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(url) || !Uri.TryCreate(url, UriKind.Absolute, out Uri validatedUri))
            {
                return BadRequest("The url field is required and must be a valid URL.");
            }

            var command = new ReduceUrlCommand
            {
                ContextRequestScheme = _context.Request.Scheme,
                ContextRequestHost = _context.Request.Host.ToString(),
                Original = url
            };

            var urlInfo = await _mediator.Send(command, cancellationToken);
            if (urlInfo == null)
            {
                return BadRequest("An error occurred while processing the request.");
            }
            return Ok(urlInfo);
        }


        [HttpGet("/{code}")]
        public async Task<ActionResult> Redirect(string code, CancellationToken cancellationToken)
        {
            var command = new RedirectQuery
            {
                Code = code
            };
            var urlInfo = await _mediator.Send(command, cancellationToken);
            if (urlInfo != null)
            {
                return Redirect(urlInfo.OriginalUrl);
            }
            return NotFound("Shorten url was not found.");
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<UrlInfoDto>>> GetAll(CancellationToken cancellationToken)
        {
            var command = new GetAllQuery();
            var urlInfos = await _mediator.Send(command, cancellationToken);

            return Ok(urlInfos);
        }

    }
}
