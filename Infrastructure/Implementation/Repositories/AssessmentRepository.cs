using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssessmentRepository(ApplicationDbContext context)
        {
            _context = context;    
        }

        public async Task<bool> AddAsync(MentalHealth mentalHealth)
        {
            await _context.AddAsync(mentalHealth);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MentalHealth>> GetAllMentalsByStudentId(int studentId)
        {
            return await _context.MentalHealthRecords.ToListAsync();
        }
    }
}
