using Application.DTO.Goals;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands.Add_New_Goal
{
    public record AddNewGoalCommand(int userId,AddNewGoalInputDto dto):IRequest<AddNewGoalResult>;
}
