using Application.DTO.Authentication;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services  
{
    public class LinkedinAuthService : ILinkedinAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public LinkedinAuthService(HttpClient client, IConfiguration cfg, UserManager<ApplicationUser> userManager, ApplicationDbContext context, ITokenGeneratorService tokenService)
        {
            _configuration = cfg;
            _httpClient = client;
            _userManager = userManager;
            _context = context;
            _tokenGeneratorService = tokenService;
        }

        public async Task<string> GetAccessToken(string code)
        {
            var values = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", _configuration["LinkedIn:RedirectUri"] },
                { "client_id", _configuration["LinkedIn:ClientId"] },
                { "client_secret", _configuration["LinkedIn:ClientSecret"] }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"LinkedIn token error: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LinkedinTokenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result.access_token;
        }

        public async Task<LoginOrRegisterUsingLinkedinResult> GetLinkedinUser(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.linkedin.com/v2/userinfo");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"LinkedIn user info error: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginOrRegisterUsingLinkedinResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<AuthModel> GetUserInformationBasedOnLinkedin(LoginOrRegisterUsingLinkedinResult linkedinUser)
        {
            var user = await _userManager.FindByEmailAsync(linkedinUser.email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = linkedinUser.email,
                    UserName = linkedinUser.email,
                    FirstName = linkedinUser.name,
                    LinkedinId = linkedinUser.sub
                };

                IdentityResult result = await _userManager.CreateAsync(user);

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
                    Name = linkedinUser.name,
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

                var newUserRoles = await _userManager.GetRolesAsync(user);
                var linkedinJwtToken = await _tokenGeneratorService.GenerateToken(user.Id, user.Email, newUserRoles);

                return new AuthModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    StudentId = newStudent.Id,
                    ExpiresON = linkedinJwtToken.ExpiresOn,
                    IsAuthenticated = true,
                    Roles = newUserRoles.ToList(),
                    Token = linkedinJwtToken.Token,
                    Username = user.UserName
                };

            }

            var existingUserRoles = await _userManager.GetRolesAsync(user);
            var existingUserJwtToken = await _tokenGeneratorService.GenerateToken(user.Id,user.Email, existingUserRoles);


            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.UserId == user.Id);
            int studentId = existingStudent?.Id ?? 0;

            if (string.IsNullOrEmpty(user.LinkedinId))
            {
                user.LinkedinId = linkedinUser.sub;
                await _userManager.UpdateAsync(user);
            }

            return new AuthModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StudentId = studentId,
                ExpiresON = existingUserJwtToken.ExpiresOn,
                IsAuthenticated = true,
                Roles = existingUserRoles.ToList(),
                Token = existingUserJwtToken.Token,
                Username = user.UserName
            };
        }
    }
}
