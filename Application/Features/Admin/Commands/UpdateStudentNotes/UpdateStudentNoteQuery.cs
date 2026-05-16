using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands.Update_student_Notes
{
    public record UpdateStudentNoteCommand(int studentId, string Note): IRequest<bool>;
}
