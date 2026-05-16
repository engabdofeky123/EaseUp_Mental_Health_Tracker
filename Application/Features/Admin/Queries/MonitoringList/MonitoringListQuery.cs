using Application.DTO.Admins;
using MediatR;

namespace Application.Features.Admin.Queries.MonitoringList
{
    public record MonitoringListQuery() : IRequest<UserMonitoringResult>;
}