using Application.DTO.Authentication;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.LoginUsingLinkedin
{
    public class LoginUsingLinkedinHandler : IRequestHandler<LoginUsingLinkedinCommand, LinkedinTokenResponse>
    {
        private readonly ILinkedinAuthService _linkedinAuthService;

        public LoginUsingLinkedinHandler(ILinkedinAuthService service)
        {
            _linkedinAuthService = service;
        }
        public async Task<LinkedinTokenResponse> Handle(LoginUsingLinkedinCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // 1. جلب الـ Access Token من جوجل
            var accessToken = await _linkedinAuthService.GetAccessToken(request.code);

            if (string.IsNullOrEmpty(accessToken))
                return null;
            // 2. جلب بيانات يوزر جوجل
            var linkedinUser = await _linkedinAuthService.GetLinkedinUser(accessToken);

            if (linkedinUser == null)
                return null;

            var authResult = await _linkedinAuthService.GetUserInformationBasedOnLinkedin(linkedinUser);

            return new LinkedinTokenResponse
            {
               access_token = authResult.Token,
                expires_in = (int)(authResult.ExpiresON - DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
