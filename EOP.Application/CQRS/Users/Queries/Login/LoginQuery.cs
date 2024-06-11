using EOP.Application.Dtos;
using EOP.Application.Exceptions;
using EOP.Application.Helpers;
using EOP.Application.Interfaces;
using EOP.Domain.Entities;
using MediatR;
using System.Net;

namespace EOP.Application.CQRS.Users.Queries.Login
{
    public record LoginQuery : IRequest<LoginResponseDto>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    internal class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTProvider _jwtProvider;
        public LoginQueryHandler(IUnitOfWork unitOfWork, IJWTProvider jwtProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x => x.Username == request.Username);

            if(user == null || !PasswordHelper.Verify(user.Password, request.Password)) throw new EOPException(HttpStatusCode.Unauthorized,"Invalid username or password");

            return new LoginResponseDto { Token = await _jwtProvider.Generate(user) };
        }
    }
}
