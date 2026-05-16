using Application.DTO.Profile;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Commands.UpdateSupervisorProfile
{
    public class UpdateSupervisorProfileHandler : IRequestHandler<UpdateSupervisorProfileCommand, GetSupervisorProfileData>
    {
        private readonly IProfileRepository _profileRepository;

        public UpdateSupervisorProfileHandler(IProfileRepository repo)
        {
            _profileRepository = repo;
        }
        public async  Task<GetSupervisorProfileData> Handle(UpdateSupervisorProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.supervisor == null)
                return null;
            return await _profileRepository.UpdateSupervisorProfileAsync(request.supervisorId, request.supervisor);
        }
    }
}
