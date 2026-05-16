using Application.Interfaces.Repositories;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Queries.Get_Goal_By_Id
{
    public class GetGoalByIdHandler : IRequestHandler<GetGoalByIdQuery, Goal>
    {
        private readonly IGoalsRepository _goalsRepository;
        public GetGoalByIdHandler(IGoalsRepository repo)
        {
            _goalsRepository = repo;
        }
        public async Task<Goal> Handle(GetGoalByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.id <= 0)
                return null;

            return await _goalsRepository.GetGoalByIdAsync(request.id);

        }
    }
}
