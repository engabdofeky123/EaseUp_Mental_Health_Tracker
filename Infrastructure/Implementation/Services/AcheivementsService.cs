using Application.DTO.Acheivements;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class AcheivementsService : IAcheivementsService
    {
        private readonly ApplicationDbContext _context;
        public AcheivementsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AssignAcheivementToStudentResult> AssignAcheivement(int studentId, int acheivementId)
        {

            var exists = await _context.StudentsAcheivements
                        .AnyAsync(sa => sa.StudentId == studentId && sa.AcheivementId == acheivementId);

            if (!exists)
            {
                var newAward = new StudentsAcheivements
                {
                    StudentId = studentId,
                    AcheivementId = acheivementId
                };

                _context.StudentsAcheivements.Add(newAward);
                await _context.SaveChangesAsync();
                return new AssignAcheivementToStudentResult
                {
                    Message = "Assigned successfully",
                    isAssigned = true
                };
            }
            return new AssignAcheivementToStudentResult
            {
                Message = "student already Assigned"
            };
        }

        public async Task<List<StudentAchievementDto>> GetAchievementsByStudentIdAsync(int studentId)
        {
            return await _context.StudentsAcheivements
            .Where(sa => sa.StudentId == studentId)
            .Join(_context.Acheivements,
                sa => sa.AcheivementId,
                a => a.Id,
                (sa, a) => new StudentAchievementDto
                {
                    Title = a.Title,
                    Description = a.Description
                }).ToListAsync();
        }
    }
}