using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Interfaces.Services
{
    public interface IGoogleAuthService
    {
        Task<LoginOrRegisterUsingGoogleResult> GetGoogleUser(string accessToken);
        Task<string> GetGoogleAccessToken(string code);
        public Task<AuthModel> GetUserInformationBasedOnGoogle(LoginOrRegisterUsingGoogleResult googleUser);
    }
}
