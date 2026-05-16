using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Queries.GetStudentById
{
    public class GetSudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Domain.Models.Student>
    {
        private readonly IProfileRepository _profileRepository;

        public GetSudentByIdHandler(IProfileRepository profileRepo)
        {
            _profileRepository = profileRepo;
        }
        public async Task<Domain.Models.Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.studentId <= 0)
                return null;
            return await _profileRepository.GetStudentById(request.studentId);
        }
    }
}
