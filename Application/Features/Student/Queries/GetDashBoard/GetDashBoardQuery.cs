using Application.DTO.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Queries.GetDashBoard
{
    public record GetDashboardQuery(int UserId) : IRequest<DashboardDto?>;
}
