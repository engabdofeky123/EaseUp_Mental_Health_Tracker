using Application.DTO.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IExerciseRepository
    {
        public Task<List<ExerciseDto>> GetExercises();
        Task SaveDB();
    }
}