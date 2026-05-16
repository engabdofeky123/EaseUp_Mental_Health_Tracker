using Application.DTO.Authentication;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.LoginUsingGoogle
{
    internal class LoginUsingGoogleHandler : IRequestHandler<LoginUsingGoogleCommand, GoogleTokenResponse>
    {
        private readonly IGoogleAuthService _googleAuthService;
        public LoginUsingGoogleHandler(IGoogleAuthService googleService)
        {
            _googleAuthService = googleService;
        }
        public async Task<GoogleTokenResponse> Handle(LoginUsingGoogleCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // 1. جلب الـ Access Token من جوجل
            var accessToken = await _googleAuthService.GetGoogleAccessToken(request.code);

            if (string.IsNullOrEmpty(accessToken))
                return null;
            // 2. جلب بيانات يوزر جوجل
            var googleUser = await _googleAuthService.GetGoogleUser(accessToken);

            if (googleUser == null)
                return null;

            var authResult = await _googleAuthService.GetUserInformationBasedOnGoogle(googleUser);

            return new GoogleTokenResponse
            {
                access_token = authResult.Token,
                expires_in = (int)(authResult.ExpiresON - DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
