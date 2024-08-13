using MediatR;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.Application.User.Commands
{
    public class LoginUserCommand : IRequest<TokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
