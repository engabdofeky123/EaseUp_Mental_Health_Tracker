using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Commands.UpdateStudent
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Domain.Models.Student>
    {
        private readonly IProfileRepository _profileRepository;

        public UpdateStudentHandler(IProfileRepository repo)
        {
            _profileRepository = repo;
        }

        public async Task<Domain.Models.Student> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.Student == null)
                return null;

            return await _profileRepository.UpdateStudentAsync(request.Student);
        }
    }
}
