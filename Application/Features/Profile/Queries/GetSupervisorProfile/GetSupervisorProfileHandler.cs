using Application.DTO.Profile;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Queries.GetSupervisorProfile
{
    public class GetSupervisorProfileHandler : IRequestHandler<GetSupervisorProfileQuery, GetSupervisorProfileData>
    {
        private readonly IProfileRepository _profileRepository;

        public GetSupervisorProfileHandler(IProfileRepository repo)
        {
            _profileRepository = repo;
        }

        public async Task<GetSupervisorProfileData> Handle(GetSupervisorProfileQuery request, CancellationToken cancellationToken)
        {
            if(request.supervisorId <= 0) 
                return null;

             return await _profileRepository.GetSupervisorProfileAsync(request.supervisorId);
        }
    }
}
