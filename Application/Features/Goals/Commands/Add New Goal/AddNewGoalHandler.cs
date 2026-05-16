using Application.DTO.Goals;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands.Add_New_Goal
{
    public class AddNewGoalHandler : IRequestHandler<AddNewGoalCommand, AddNewGoalResult>
    {
        private readonly IGoalsRepository _studentRepository;
        private readonly IGetStudentByUserIdService _getStudentByUserIdService;

        public AddNewGoalHandler(IGoalsRepository repo, IGetStudentByUserIdService service)
        {
            _studentRepository = repo;
            _getStudentByUserIdService = service;
        }

        public async Task<AddNewGoalResult> Handle(AddNewGoalCommand request, CancellationToken cancellationToken)
        {
            var student = await _getStudentByUserIdService.GetStudentAsyncByUserID(request.userId, cancellationToken);
            if (student == null)
                return new AddNewGoalResult { Message = "Invalid userId / student Id" };
            return await _studentRepository.AddNewGoal(request.dto, student.Id,cancellationToken);
        }
    }
}