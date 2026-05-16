using Application.DTO.Profile;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Commands.UpdateStudentProfile
{
    public class UpdateStudentProfileHandler : IRequestHandler<UpdateStudentProfileCommand, GetStudentProfileData>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IGetStudentByUserIdService _getStudentByUserIdService;

        public UpdateStudentProfileHandler(IProfileRepository repo, IGetStudentByUserIdService service)
        {
            _profileRepository = repo;
            _getStudentByUserIdService = service;
        }

        public async Task<GetStudentProfileData> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.dto == null)
                return null;
            var student = await _getStudentByUserIdService.GetStudentAsyncByUserID(request.userId,cancellationToken);
            return await _profileRepository.UpdateStudentProfileAsync(student.Id, request.dto);
        }
    }
}
