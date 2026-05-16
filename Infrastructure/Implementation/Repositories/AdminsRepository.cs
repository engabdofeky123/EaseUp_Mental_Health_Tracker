using Application.DTO.Admins;
using Application.DTO.Notifications;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IImageService _imageService;
        public AdminsRepository(ApplicationDbContext context, UserManager<ApplicationUser> userMnager, IImageService imageService)
        {
            _context = context;
            _userManager = userMnager;
            _imageService = imageService;
        }

        public async Task<AdminsOverviewResult> AdminsOverview(CancellationToken cancellationToken)
        {
            return await _context.Students
                .GroupBy(s => 1)
                .Select(g => new AdminsOverviewResult
                {
                    Users = g.Count(),
                    ActiveUsers = g.Where(x=> x.IsActive == true).Count(),
                    CrisisUsers = 0  //g.Where(x => x.IsInCrisis == true).Count()()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<UserMonitoringResult> UserMonitoringList(CancellationToken cancellationToken)
        {
            var students = await _context.Students.ToListAsync(cancellationToken);
            var userIds = students.Select(s => s.UserId).ToList();

            var users = await _userManager.Users.Where(u => userIds.Contains(u.Id))
                        .Select(u => new { u.Id, u.Email }).ToListAsync(cancellationToken);

            var result = students.Select(s => new UserMonitoringItem
            {
                Id = s.Id,
                Name = s.Name,
                imageUrl = s.ProfilePictureUrl,
                Email =  users.FirstOrDefault(u => u.Id == s.UserId)?.Email,
                Status = s.IsActive ? "Active" : "Inactive"
                // ,LastActive = s.LastActiveDate > DateTime.UtcNow.AddDays(-7)
            }).ToList();

            return new UserMonitoringResult { Students = result };
        }

        public async Task<StudentProfileInformationResutl> GetStudentProfileInformation(int studentId, CancellationToken cancellationToken)
        {
            var student = await _context.Students.Include(u => u.Goals).FirstOrDefaultAsync(x => x.Id == studentId);
            if (student == null)
                return null;
            var goalsAcheivedDetails = student.Goals.Select(x => new GoalsAchievedDto
            {
                Id = x.Id,
                Title = x.Title,
                DeadLine = x.Deadline,
                DoneAt = x.DoneAt,
                Priority = x.GoalPriority.ToString(),
            }).ToList();
            return new StudentProfileInformationResutl
            {
                Id = student.Id,
                Name = student.Name,
                Email = (await _userManager.FindByIdAsync(student.UserId.ToString()))?.Email,
                PictureUrl = student.ProfilePictureUrl,
                University = student.University,
                AcademicYear = student.AcademicYear,
                CurrentGPA = student.CurrentGPA,
                Department = student.Department,
                GoalsCount = student.Goals.Count(),
                GoalsAchieved = student.Goals.Where(x => x.IsCompleted == true).Count(),
                HasScolarship = student.HasScolarship,
                Total_Exercise_Score = student.Total_Exercise_Score ,
                GoalsAchieveds = goalsAcheivedDetails
            };
        }

        public async Task<List<AdminManagementItem>> GetAdminManagementData(CancellationToken cancellationToken)
        {
            var admins = await _userManager.GetUsersInRoleAsync("admin");
            var supervisors = await _userManager.GetUsersInRoleAsync("supervisor");

            var result = admins.Concat(supervisors).OrderBy(u => u.Id)
                .Select(u => new AdminManagementItem
                {
                    Id = u.Id,
                    Email = u.Email,
                    LastActive = default,
                    Name = u.FirstName + " " + u.LastName,
                    Status = admins.Contains(u)? "admin" : "supervisor"
                }).ToList();

            return result;
        }

        public async Task<ViewProfileInformationResult> GetProfileInformation(int userId)
        {
            var admin = await _userManager.FindByIdAsync(userId.ToString());
            var isUserAAdmin = await _userManager.IsInRoleAsync(admin,"admin");
            if (!isUserAAdmin)
                return new ViewProfileInformationResult { IsAdmin = false , Message = "UnAuthorized User"};
            return new ViewProfileInformationResult 
            {
                IsAdmin = true ,
                Email = admin.Email,
                Name = admin.FirstName + " " + admin.LastName,
                imageUrl = admin.ImageUrl,
                Message = "Data retreived successfully"
            };
        }

        public async Task<UpdateProfileInformationData> UpdateProfileInformation(int userId , UpdateProfileInformationData input)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return null;

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                var names = input.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                user.FirstName = names.FirstOrDefault();
                user.LastName = names.Skip(1).FirstOrDefault();
            }

            // Change Password
            if (!string.IsNullOrEmpty(input.NewPassword) && !string.IsNullOrEmpty(input.CurrentPassword))
            {
                var changeResult = await _userManager.ChangePasswordAsync(
                    user,
                    input.CurrentPassword,
                    input.NewPassword
                );

                if (!changeResult.Succeeded)
                {
                    return new UpdateProfileInformationData
                    {
                        Errors = changeResult.Errors.Select(e => e.Description).ToList()
                    };
                }
            }

            // Upload Image
            if (input.ImageFile != null)
            {
                var path = await _imageService.UploadAsync(input.ImageFile);

                if (path != null || !string.IsNullOrEmpty(path))
                    user.ImageUrl = path;
            }

            // Update user
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UpdateProfileInformationData
                {
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            // Return updated data
            return new UpdateProfileInformationData
            {
                Name = $"{user.FirstName} {user.LastName}",
                ImageUrl = user.ImageUrl ,
                Email = user.Email 
            };
        }
    }
}
