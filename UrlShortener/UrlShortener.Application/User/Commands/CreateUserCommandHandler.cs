using AutoMapper;
using MediatR;
using UrlShortener.Application.ViewModels;
using UrlShortener.Domain.Interfaces.Auth;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Application.User.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Generate(request.Password);

            var user = new Domain.Entities.User
            {
                Email = request.Email,
                PasswordHash = hashedPassword,
                Name = request.Name,
                Role = await _usersRepository.GetRoleByName("Default")
            };
            await _usersRepository.Add(user);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
