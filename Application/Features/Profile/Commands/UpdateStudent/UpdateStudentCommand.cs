using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;

namespace Application.Features.Profile.Commands.UpdateStudent
{
    public record UpdateStudentCommand( Domain.Models.Student Student): IRequest<Domain.Models.Student>;
}