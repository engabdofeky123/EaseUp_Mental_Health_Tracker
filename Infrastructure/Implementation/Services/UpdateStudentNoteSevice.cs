using Application.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class UpdateStudentNoteSevice : IUpdateStudentNoteSevice
    {
        private readonly ApplicationDbContext _context;

        public UpdateStudentNoteSevice(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateNoteForStudent(int studentId, string note, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentId,cancellationToken);

            if (student == null) 
                return  false;

            student.AdminNotes = note;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
