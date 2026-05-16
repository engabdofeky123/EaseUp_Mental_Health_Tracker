using Application.DTO.Goals;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Queries.GetAllActiveGoals
{
    public class GetAllActiveGoalsHandler : IRequestHandler<GetAllActiveGoalsQuery, GetActiveGoalsResult>
    {
        private readonly IGoalsRepository _studentRepository;
        private readonly IGetStudentByUserIdService _getStudentByUserIdService;

        public GetAllActiveGoalsHandler(IGoalsRepository repo , IGetStudentByUserIdService service)
        {
            _studentRepository = repo;
            _getStudentByUserIdService = service;
        }

        public async Task<GetActiveGoalsResult> Handle(GetAllActiveGoalsQuery request, CancellationToken cancellationToken)
        {
            var student = await _getStudentByUserIdService.GetStudentAsyncByUserID(request.userId,cancellationToken);
            if (student == null) 
                return new GetActiveGoalsResult { Message = "Invalid userId / student Id"};
            var goalsResult = await _studentRepository.GetActiveGoals(student.Id, cancellationToken);
            return new GetActiveGoalsResult { Message = "Success" , ActiveGoals = goalsResult.ActiveGoals , IsSuccess = true };
        }
    }
}
