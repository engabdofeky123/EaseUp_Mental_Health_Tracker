using Application.DTO.Authentication;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Infrastructure.Implementation.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GoogleAuthService(HttpClient httpClient, IConfiguration configuration, UserManager<ApplicationUser> userManager,
            ApplicationDbContext applicationDbContext, ITokenGeneratorService tokenGeneratorService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userManager = userManager;
            _context = applicationDbContext;
            _tokenGeneratorService = tokenGeneratorService;
        }
        public async Task<string> GetGoogleAccessToken(string code)
        {
            var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _configuration["Google:ClientId"] },
                { "client_secret", _configuration["Google:ClientSecret"] },
                { "redirect_uri", _configuration["Google:RedirectUri"] },
                { "grant_type", "authorization_code" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Google token error: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GoogleTokenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result.access_token;
        }

        public async Task<LoginOrRegisterUsingGoogleResult> GetGoogleUser(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v3/userinfo");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Google user info error: {error}");
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginOrRegisterUsingGoogleResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<AuthModel> GetUserInformationBasedOnGoogle(LoginOrRegisterUsingGoogleResult googleUser)
        {
            var user = await _userManager.FindByEmailAsync(googleUser.email);
            Student studentProfile = null;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = googleUser.email,
                    UserName = googleUser.email,
                    FirstName = googleUser.name,
                    GoogleId = googleUser.sub
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    var errorList = string.Join(" ",
                        result.Errors.Select(e => e.Description));
                    return new AuthModel
                    {
                        Message = $"There are some errors: {errorList}"
                    };
                }

                await _userManager.AddToRoleAsync(user, "student");

                var newStudent = new Student
                {
                    UserId = user.Id,
                    Name = googleUser.name,
                    Age = 20,
                    Gender = "Not specified",
                    University = "Not specified",
                    Department = "Not specified",
                    AcademicYear = 1,
                    CurrentGPA = 0.0,
                    IsActive = true,
                    HasScolarship = false,
                    Total_Exercise_Score = 0
                };
                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();
            }
            else
            {
                 studentProfile = await _context.Students.FirstOrDefaultAsync(s => s.UserId == user.Id);

                if (string.IsNullOrEmpty(user.GoogleId))
                {
                    user.GoogleId = googleUser.sub;
                    await _userManager.UpdateAsync(user);
                }
            }
            var roles = await _userManager.GetRolesAsync(user);
            var tokenResult = await _tokenGeneratorService.GenerateToken(user.Id, user.Email, roles);

            // تحويل الـ JwtSecurityToken إلى String

            return new AuthModel
            {
                IsAuthenticated = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                UserId = user.Id,
                StudentId = studentProfile?.Id ?? 0, 
                Roles = roles.ToList(),
                Token = tokenResult.Token,
                ExpiresON = tokenResult.ExpiresOn,
                Message = "Success"
            };

        }
    }
}