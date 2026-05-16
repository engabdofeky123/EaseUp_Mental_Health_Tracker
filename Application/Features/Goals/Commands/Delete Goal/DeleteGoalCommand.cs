using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Commands.Delete_Goal
{
    public record DeleteGoalCommand(int goalId): IRequest<bool>;
}