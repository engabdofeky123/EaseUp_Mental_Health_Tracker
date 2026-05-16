using Application.Common.Settings;
using Application.Interfaces.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        public TokenGeneratorService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> options)
        {
            _userManager = userManager;
            _jwtOptions = options.Value;
        }
        public async Task<(string Token, DateTime ExpiresOn)> GenerateToken(int userId, string email, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null || user.Email != email)
                throw new Exception("User not found");

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roleClaims = roles
                    .Select(role => new Claim(ClaimTypes.Role, role))
                    .ToList();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("userID", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            
            var finalJwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtOptions.DurationInDays),
                signingCredentials: creds
            );
            return (new JwtSecurityTokenHandler().WriteToken(finalJwtToken), finalJwtToken.ValidTo);
        }
    }
}
