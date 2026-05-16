using Application.DTO.Profile;
using Application.DTO.Student;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Commands.UpdateStudentProfile
{
    public record UpdateStudentProfileCommand( int userId, UpdateStudentDto dto ): IRequest<GetStudentProfileData>; 
}
