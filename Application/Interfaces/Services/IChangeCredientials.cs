using Application.DTO.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IChangeCredientials
    {
        public Task<ChangeCredentialsResult> ChangePasswordAsync(int userId, ChangePasswordRequest request);
        public Task<ChangeCredentialsResult> ChangeEmailAsync(int userId, ChangeEmailRequest request);
    }
}