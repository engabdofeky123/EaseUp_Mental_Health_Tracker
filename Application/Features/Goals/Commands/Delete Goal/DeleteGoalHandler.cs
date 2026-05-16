using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Commands.Delete_Goal
{
    public class DeleteGoalHandler : IRequestHandler<DeleteGoalCommand, bool>
    {
        private readonly IGoalsRepository _goalsRepository;

        public DeleteGoalHandler(IGoalsRepository repo)
        {
            _goalsRepository = repo;
        }

        public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
        {
            if (request == null | request.goalId <= 0)
                return false;

            await _goalsRepository.DeleteGoalAsync(request.goalId);
            return true;
        }
    }
}
