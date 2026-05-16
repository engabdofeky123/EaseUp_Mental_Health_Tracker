using Application.DTO.Goals;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Commands.Update_Goal
{
    public record UpdateGoalCommand(int id, UpdateGoalDto goal): IRequest<bool>;
}
