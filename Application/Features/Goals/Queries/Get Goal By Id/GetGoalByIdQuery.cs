using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Goals.Queries.Get_Goal_By_Id
{
    public record GetGoalByIdQuery(int id): IRequest<Goal>;
}
