using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUpdateStudentNoteSevice
    {
        public Task<bool> UpdateNoteForStudent(int studentId, string note, CancellationToken cancellationToken);
    }
}
