using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Commands.Toggle_Item
{
    public class ToggleItemHandler : IRequestHandler<ToggleItemCommand, bool>
    {
        private IGoalsRepository _goalsRepository;
        private IAcheivementsService _acheivementsService;
        public ToggleItemHandler(IGoalsRepository repo, IAcheivementsService acheivementsService)
        {
            _goalsRepository = repo;
            _acheivementsService = acheivementsService;
        }
        public async Task<bool> Handle(ToggleItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null| request.goalId <= 0 | request.studentId <= 0 | request.itemId <= 0)
                return false;
            await _goalsRepository.ToggleItemAsync(request.itemId,request.studentId,request.goalId);

            var goal = await _goalsRepository.GetGoalByIdAsync(request.goalId);
            if (goal.IsCompleted)
                await _acheivementsService.AssignAcheivement(request.studentId, 1);
            return true;
        }
    }
}
