using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto registerDto);
        Task<AuthModel> LoginAsync(LoginDto loginDto);

        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

        Task<bool> AddToRoleAsync(int userId, string roleName);
    }
}