using Application.DTO.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.AdminManagementInformation
{
    public class AdminManagementHandler : IRequestHandler<AdminManagementQuery, List<AdminManagementItem>>
    {
        private readonly IAdminsRepository _adminsRepository;
        public AdminManagementHandler(IAdminsRepository adminsRepo)
        {
            _adminsRepository = adminsRepo;
        }
        public async Task<List<AdminManagementItem>> Handle(AdminManagementQuery request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            var result = await _adminsRepository.GetAdminManagementData(cancellationToken);

            return result;
        }
    }
}
