using Application.DTO.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.Overview
{
    public class AdminOverviewHandler : IRequestHandler<AdminOverviewQuery, AdminsOverviewResult>
    {
        private readonly IAdminsRepository _adminRepository;
        public AdminOverviewHandler(IAdminsRepository adminRepo)
        {
            _adminRepository = adminRepo;
        }
        public async Task<AdminsOverviewResult> Handle(AdminOverviewQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (cancellationToken.IsCancellationRequested)
                return null;

            var result = await _adminRepository.AdminsOverview(cancellationToken);
            return result;
        }
    }
}
