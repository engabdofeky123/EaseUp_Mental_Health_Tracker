using Application.DTO.Profile;
using Application.DTO.Student;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;
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
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IImageService _imageService;

        public ProfileRepository(ApplicationDbContext context, UserManager<ApplicationUser> usermanager, IImageService imgService)
        {
            _context = context;
            userManager = usermanager;
            _imageService = imgService;
        }

        // ✅ دالة جلب الطالب بواسطة ID
        public async Task<Student> GetStudentById(int studentId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }

        // ✅ دالة تحديث الطالب
        public async Task<Student> UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        // ✅ إضافة دالة جلب الطالب بواسطة UserId (من الـ Token)
        public async Task<Student> GetStudentByUserId(int userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<GetStudentProfileData> GetStudentProfileAsync(int studentId)
        {
            return await _context.Students.Where(s => s.Id == studentId)
                .Select(s => new GetStudentProfileData
                {
                    Name = s.Name,
                    ProfilePictureUrl = s.ProfilePictureUrl,
                    Age = s.Age,
                    University = s.University,
                    Department = s.Department,
                    Title = "Student",
                    JobTitle = "Student"
                }).FirstOrDefaultAsync();
        }

        public async Task<GetSupervisorProfileData> GetSupervisorProfileAsync(int supervisorId)
        {
            return await _context.Supervisors.Where(s => s.Id == supervisorId).Include(s => s.Students)
                .Select(s => new GetSupervisorProfileData
                {
                    Name = s.Name,
                    University = s.University,
                    IsActive = s.IsActive,
                    TotalStudents = s.Students.Count(),
                    CreatedAt = s.CreatedAt,
                    Department = "Not specified",
                    Age = 0
                }).FirstOrDefaultAsync();
        }

        public async Task<GetStudentProfileData> UpdateStudentProfileAsync(int studentId, UpdateStudentDto studentDto)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.Id == studentId);
            if (existingStudent == null)
                return null;

            existingStudent.Name = studentDto.Name;
            existingStudent.Age = studentDto.Age;
            existingStudent.University = studentDto.University;
            existingStudent.Department = studentDto.Department;

            // call IImageService

            if(studentDto.Image != null)
            {
                var StudentImageUrl = await _imageService.UploadAsync(studentDto.Image);
                if (!string.IsNullOrEmpty(studentDto.ImageUrl))
                {
                    existingStudent.ProfilePictureUrl = studentDto.ImageUrl;
                }
            }
            
            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();

            return new GetStudentProfileData
            {
                Name = existingStudent.Name,
                ProfilePictureUrl = existingStudent.ProfilePictureUrl,
                Age = existingStudent.Age,
                University = existingStudent.University,
                Department = existingStudent.Department,
                Title = "Student",
                JobTitle = "Student"
            };
        }

        public async Task<GetSupervisorProfileData> UpdateSupervisorProfileAsync(int supervisorId, Supervisor supervisor)
        {
            var existingSupervisor = _context.Supervisors.FirstOrDefault(s => s.Id == supervisorId);
            if (existingSupervisor == null)
                throw new Exception("Supervisor not found");

            existingSupervisor.Name = supervisor.Name;
            existingSupervisor.PhoneNumber = supervisor.PhoneNumber;
            existingSupervisor.University = supervisor.University;
            existingSupervisor.IsActive = supervisor.IsActive;

            _context.Supervisors.Update(existingSupervisor);
            await _context.SaveChangesAsync();

            return new GetSupervisorProfileData
            {
                Name = existingSupervisor.Name,
                University = existingSupervisor.University,
                IsActive = existingSupervisor.IsActive,
                TotalStudents = existingSupervisor.Students.Count(),
                CreatedAt = existingSupervisor.CreatedAt,
                Department = "Not specified",
                Age = 0
            };
        }
    }
}
