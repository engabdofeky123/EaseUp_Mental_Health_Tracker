using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAssessmentRepository
    {
        Task<bool> AddAsync(MentalHealth mentalHealth);
        Task<List<MentalHealth>> GetAllMentalsByStudentId(int studentId);
    }
}
