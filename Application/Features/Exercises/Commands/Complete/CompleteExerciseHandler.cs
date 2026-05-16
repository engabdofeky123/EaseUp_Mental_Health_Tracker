using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exercises.Commands.Complete
{
    public class CompleteExerciseHandler : IRequestHandler<CompleteExerciseCommand, bool>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IGetStudentByUserIdService _getStudentByUserIdService;
        private readonly IAcheivementsService _getAcheivementsService;


        public CompleteExerciseHandler(IExerciseRepository repo, IGetStudentByUserIdService service, IAcheivementsService getAcheivementsService)
        {
            _exerciseRepository = repo;
            _getStudentByUserIdService = service;
            _getAcheivementsService = getAcheivementsService;
        }

        public async Task<bool> Handle(CompleteExerciseCommand request, CancellationToken cancellationToken)
        {
           var student =  await _getStudentByUserIdService.GetStudentAsyncByUserID(request.userId, cancellationToken);

            if (student == null)
                return false;

            student.Total_Exercise_Score += 5;
            if(student.Total_Exercise_Score >= 25) // did exercises more 5 times
                await _getAcheivementsService.AssignAcheivement(student.Id, 3);
            await _exerciseRepository.SaveDB();

            return true;
        }
    }
}