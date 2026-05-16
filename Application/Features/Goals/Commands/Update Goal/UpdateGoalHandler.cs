using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Commands.Update_Goal
{
    public class UpdateGoalHandler : IRequestHandler<UpdateGoalCommand, bool>
    {
        private readonly IGoalsRepository _goalsRepository;
        public UpdateGoalHandler(IGoalsRepository repo)
        {
            _goalsRepository = repo;
        }

        public async Task<bool> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
        {
            if (request == null | request.id <= 0 | request.goal == null) 
                    return false;

            await _goalsRepository.UpdateGoalAsync(request.id, request.goal);
            return true;
        }
    }
}
