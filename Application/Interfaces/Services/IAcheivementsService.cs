using Application.DTO.Acheivements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAcheivementsService
    {
        Task<List<StudentAchievementDto>> GetAchievementsByStudentIdAsync(int studentId);
        public Task<AssignAcheivementToStudentResult> AssignAcheivement(int studentId, int acheivementId);
    }
}