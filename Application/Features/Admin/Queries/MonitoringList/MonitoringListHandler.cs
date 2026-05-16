using Application.DTO.Admins;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Admin.Queries.MonitoringList
{
    public class MonitoringListHandler : IRequestHandler<MonitoringListQuery, UserMonitoringResult>
    {
        private readonly IAdminsRepository _adminsRepository;
        public MonitoringListHandler(IAdminsRepository adminRepo)
        {
            _adminsRepository = adminRepo;
        }
        public async Task<UserMonitoringResult> Handle(MonitoringListQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (cancellationToken.IsCancellationRequested)
                return null;
           
            return await _adminsRepository.UserMonitoringList(cancellationToken);
        }
    }
}