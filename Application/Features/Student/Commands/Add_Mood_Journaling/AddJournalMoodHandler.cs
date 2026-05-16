using Application.DTO.Student.Mood_Journal;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands.Add_Mood_Journaling
{
    public class AddJournalMoodHandler : IRequestHandler<AddJournalMoodCommand, AddMoodJournalResult>
    {
        private readonly IStudentRepository _studentsRepository;
        private readonly IGetStudentByUserIdService _getStudentByUserIdService; 

        public AddJournalMoodHandler(IStudentRepository repository, IGetStudentByUserIdService service)
        {
            _studentsRepository = repository;
            _getStudentByUserIdService = service;
        }
        public async Task<AddMoodJournalResult> Handle(AddJournalMoodCommand request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            var student = await _getStudentByUserIdService.GetStudentAsyncByUserID(request.userId, cancellationToken);
            if (student == null)
                return new AddMoodJournalResult { Message = "Invalid User or Student not registered.", isSuccess = false };
            
            return await _studentsRepository.AddMoodJournal(request.moodInput,student.Id,cancellationToken);
        }
    }
}