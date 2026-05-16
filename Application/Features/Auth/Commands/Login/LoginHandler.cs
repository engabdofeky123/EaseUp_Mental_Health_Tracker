using Application.DTO.Authentication;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthModel>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<AuthModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.dto == null)
                throw new ArgumentNullException(nameof(request.dto));

            var result = await _authService.LoginAsync(request.dto);
            return result;
        }
    }
}
