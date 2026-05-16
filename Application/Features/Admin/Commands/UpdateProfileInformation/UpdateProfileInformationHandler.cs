using Application.DTO.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands.UpdateProfileInformation
{
    public class UpdateProfileInformationHandler : IRequestHandler<UpdateProfileInformationCommand, UpdateProfileInformationData>
    {
        private readonly IAdminsRepository _adminsRepository;

        public UpdateProfileInformationHandler(IAdminsRepository repo)
        {
            _adminsRepository = repo;
        }
        public async Task<UpdateProfileInformationData> Handle(UpdateProfileInformationCommand request, CancellationToken cancellationToken)
        {
            if (request == null | request.dto == null) 
                throw new NullReferenceException();
            if(request.userId < 0)
                throw new Exception("Invalid ID");
            var result = await _adminsRepository.UpdateProfileInformation(request.userId, request.dto);
            if (result == null)
                return null;
            return result;

        }   
    }
}
