using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands.Update_student_Notes
{
    public class UpdateStudentNoteHandler : IRequestHandler<UpdateStudentNoteCommand, bool>
    {
        private readonly IUpdateStudentNoteSevice _updateStudentNoteSevice;

        public UpdateStudentNoteHandler(IUpdateStudentNoteSevice updateNoteService)
        {
            _updateStudentNoteSevice = updateNoteService;
        }

        public async Task<bool> Handle(UpdateStudentNoteCommand request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            var result = await _updateStudentNoteSevice.UpdateNoteForStudent(request.studentId, request.Note, cancellationToken); 
            return result;
        }
    }
}