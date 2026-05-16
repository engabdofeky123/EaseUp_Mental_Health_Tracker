using Application.DTO.Admins;
using Application.DTO.Authentication;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Implementation.Services
{
    public class AddNewAdminService : IAddNewAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AddNewAdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ITokenGeneratorService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenGeneratorService = tokenService;
        }

        public async Task<AuthModel> AddNewAdmin(AddNewAdminDto dto)
        {
            if (dto == null)
                return new AuthModel { Message ="Null !! where are inputs ?!"};

            var newApplicationUser = new ApplicationUser
            {
                FirstName = dto.FullName.Split(" ")[0],
                LastName = dto.FullName.Split(" ")[1],
                Email = dto.Email,
                UserName = dto.Email.Split('@')[0]
            };

            var result = await _userManager.CreateAsync(newApplicationUser, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            var resultOfRoleAssignment = await _userManager.AddToRoleAsync(newApplicationUser, dto.Role);
            if (!resultOfRoleAssignment.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            var (token,expire) = await _tokenGeneratorService.GenerateToken(newApplicationUser.Id,newApplicationUser.Email, new List<string>() {dto.Role });

            if(dto.Role == "supervisor")
            {
                var supervisor = new Supervisor
                {
                    UserId = newApplicationUser.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    PhoneNumber = newApplicationUser.PhoneNumber,
                    Name = newApplicationUser.FirstName + " " + newApplicationUser.LastName
                };
                await _context.Supervisors.AddAsync(supervisor);
                await _context.SaveChangesAsync();
            }

            return new AuthModel
            {
                Email = dto.Email,
                Password = dto.Password,
                FirstName = newApplicationUser.FirstName,
                LastName = newApplicationUser.LastName,
                IsAuthenticated = true,
                Username = newApplicationUser.UserName,
                ExpiresON = expire,
                Message = "Assigned Successfully",
                Roles = new List<string>() { dto.Role},
                Token = token,
                UserId = newApplicationUser.Id
            };
        }
    }
}