using Application.DTO.Dashboard;
using Application.DTO.Goals;
using Application.DTO.Student;
using Application.DTO.Student.Mood_Journal;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        public Task<int> AddStudentAsync(Student student);
        public Task<Student> GetStudentByIdAsync(int studentId);
        public Task<IEnumerable<Student>> GetAllStudentsAsync();
        public Task UpdateStudentAsync(Student student);
        public Task DeleteStudentAsync(Guid studentId);
        public Task<AddMoodJournalResult> AddMoodJournal(AddJournalMoodInputDto dto, int studentId, CancellationToken cancellationToken);
        public Task<DashboardDto?> GetDashboardDataAsync(int studentId, CancellationToken cancellationToken);

    }
}
