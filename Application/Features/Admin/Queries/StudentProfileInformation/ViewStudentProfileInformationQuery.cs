using Application.DTO.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Queries.StudentProfileInformation
{
    public record ViewStudentProfileInformationQuery(int studentId): IRequest<StudentProfileInformationResutl>;
}
