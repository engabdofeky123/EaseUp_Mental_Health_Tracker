using Application.DTO.Profile;
using Application.DTO.Student;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        public Task<GetStudentProfileData> GetStudentProfileAsync(int studentId);
        public Task<GetSupervisorProfileData> GetSupervisorProfileAsync(int supervisorId);
        public Task<GetStudentProfileData> UpdateStudentProfileAsync(int studentId, UpdateStudentDto studentDto);
        public Task<GetSupervisorProfileData> UpdateSupervisorProfileAsync(int supervisorId, Supervisor supervisor);

        // ✅ الدوال المطلوبة لرفع الصورة 
        Task<Student> GetStudentById(int studentId);
        Task<Student> UpdateStudentAsync(Student student);

        // ✅ إضافة دالة جديدة للبحث باستخدام userId
        Task<Student> GetStudentByUserId(int userId);
    }
}