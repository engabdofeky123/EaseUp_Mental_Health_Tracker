using Application.DTO.Authentication;
using Application.Interfaces.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGeneratorService _tokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenGeneratorService _tokenGenerator)
        {
            _userManager = userManager;
            this._tokenGenerator = _tokenGenerator;
        }

        public Task<bool> AddToRoleAsync(int userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthModel> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return new AuthModel { Message = "Invalid email or password" };

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenGenerator.GenerateToken(user.Id , user.Email, roles);

            return new AuthModel
            {
                IsAuthenticated = true,
                Token = token.Token,
                Email = user.Email,
                UserId = user.Id,
                Roles = roles.ToList(),
                ExpiresON = token.ExpiresOn
            };

        }

        public async Task<AuthModel> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null) 
                return new AuthModel { Message = "Email already exists" };

            var user = new ApplicationUser
            {
                UserName = dto.Email.Split('@')[0],
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            var assignUserToRole = await _userManager.AddToRoleAsync(user, "student");

            if (!assignUserToRole.Succeeded)
            {
                var errors = string.Join(", ", assignUserToRole.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            var( token, duration) = await _tokenGenerator.GenerateToken(user.Id,user.Email, new List<string> { "student" });

            return new AuthModel
            {
                IsAuthenticated = true,
                Token = token,
                Email = user.Email,
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ExpiresON = duration
            };
        }

        public Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
