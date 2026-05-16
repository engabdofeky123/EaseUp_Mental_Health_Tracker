using Application.DTO.Goals;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Queries.GetAllActiveGoals
{
    public record GetAllActiveGoalsQuery(int userId): IRequest<GetActiveGoalsResult>;
}
