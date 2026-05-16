using Application.DTO.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Student.Commands.RegisterNewStudent
{
    public record RegisterNewStudentCommand(RegisterDto dto): IRequest<AuthModel>;
}