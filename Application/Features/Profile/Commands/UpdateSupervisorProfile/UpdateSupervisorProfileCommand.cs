using Application.DTO.Profile;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Profile.Commands.UpdateSupervisorProfile
{
    public record UpdateSupervisorProfileCommand(int supervisorId, Supervisor supervisor): IRequest<GetSupervisorProfileData>;
}
