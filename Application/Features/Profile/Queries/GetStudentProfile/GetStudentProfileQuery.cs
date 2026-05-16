using Application.DTO.Profile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Queries.GetStudentProfile
{
    public record GetStudentProfileQuery(int studentId): IRequest<GetStudentProfileData>;
}
