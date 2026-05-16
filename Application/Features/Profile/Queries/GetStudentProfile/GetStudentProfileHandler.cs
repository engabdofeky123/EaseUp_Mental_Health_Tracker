using Application.DTO.Profile;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Queries.GetStudentProfile
{
    public class GetStudentProfileHandler : IRequestHandler<GetStudentProfileQuery, GetStudentProfileData>
    {
        private readonly IProfileRepository _profileRepository;

        public GetStudentProfileHandler(IProfileRepository repo)
        {
            _profileRepository = repo;
        }

        public async Task<GetStudentProfileData> Handle(GetStudentProfileQuery request, CancellationToken cancellationToken)
        {
            if (request.studentId <=0 ) 
                return null;
            return await _profileRepository.GetStudentProfileAsync(request.studentId);
        }
    }
}
