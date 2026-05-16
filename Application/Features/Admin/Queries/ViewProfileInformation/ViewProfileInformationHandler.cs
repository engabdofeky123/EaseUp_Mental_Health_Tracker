using Application.DTO.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.ViewProfileInformation
{
    public class ViewProfileInformationHandler : IRequestHandler<ViewProfileInformationQuery, ViewProfileInformationResult>
    {
        private readonly IAdminsRepository _adminsRepository;

        public ViewProfileInformationHandler(IAdminsRepository repo)
        {
            _adminsRepository = repo;       
        }

        public async Task<ViewProfileInformationResult> Handle(ViewProfileInformationQuery request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new Exception("Can not send the request to 'ViewProfileInformationHandler' ");
            var result = await _adminsRepository.GetProfileInformation(request.id);
            return result;
        }
    }
}
