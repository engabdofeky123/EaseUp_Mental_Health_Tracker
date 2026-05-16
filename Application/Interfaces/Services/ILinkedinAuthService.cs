using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ILinkedinAuthService
    {
        Task<LoginOrRegisterUsingLinkedinResult> GetLinkedinUser(string accessToken);
        Task<string> GetAccessToken(string code);
        public Task<AuthModel> GetUserInformationBasedOnLinkedin(LoginOrRegisterUsingLinkedinResult linkedinUser);


    }
}
