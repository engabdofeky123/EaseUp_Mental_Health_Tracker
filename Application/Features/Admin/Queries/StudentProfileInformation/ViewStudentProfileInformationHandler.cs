using Application.DTO.Admins;
using Application.Interfaces.Repositories;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.StudentProfileInformation
{
    public class ViewStudentProfileInformationHandler : IRequestHandler<ViewStudentProfileInformationQuery, StudentProfileInformationResutl>
    {
        private readonly IAdminsRepository _adminsRepository;

        public ViewStudentProfileInformationHandler(IAdminsRepository adminRepo)
        {
            _adminsRepository = adminRepo;
        }
        public async Task<StudentProfileInformationResutl> Handle(ViewStudentProfileInformationQuery request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            if(request.studentId <= 0)
                throw new Exception($"Invalid student ID `{request.studentId}`") ;

            var result = await _adminsRepository.GetStudentProfileInformation(request.studentId ,cancellationToken);
            return result;
        }
    }
}
