using AutoMapper;
using MediatR;
using UrlShortener.Application.ViewModels;
using UrlShortener.Domain.Interfaces.Auth;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Application.User.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IMapper mapper, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }
        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByEmail(request.Email);
            if (user != null)
            {
                var result = _passwordHasher.Verify(request.Password, user.PasswordHash);
                if (result)
                {
                    var token = _jwtProvider.GenerateToken(user);
                    var tokenDto = new TokenDto
                    {
                        Token = token,
                    };
                    return tokenDto;
                }
            }
            return null;
        }
    }
}
