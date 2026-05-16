using Application.DTO.Exercises;
using Application.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExerciseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ExerciseDto>> GetExercises()
        {
            return await _context.Exercises
                .Select(e => new ExerciseDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Time = e.Time,
                    Tag = e.Tag,
                    Category = e.Category,
                    Image = e.Image,
                    Video = e.Video
                })
                .ToListAsync();
        }
        public async Task SaveDB()
        {
            await _context.SaveChangesAsync();
        }

    }
}
