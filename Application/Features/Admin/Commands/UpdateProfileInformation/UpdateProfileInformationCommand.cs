using Application.DTO.Admins;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Admin.Commands.UpdateProfileInformation
{
    public record UpdateProfileInformationCommand(int userId,UpdateProfileInformationData dto):IRequest<UpdateProfileInformationData>;
}
