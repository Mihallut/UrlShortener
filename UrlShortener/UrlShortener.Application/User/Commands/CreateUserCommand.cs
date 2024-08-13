using MediatR;
using UrlShortener.Application.ViewModels;

namespace UrlShortener.Application.User.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
