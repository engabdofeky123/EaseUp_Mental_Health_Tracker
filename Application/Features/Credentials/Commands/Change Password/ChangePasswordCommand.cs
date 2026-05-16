using Application.DTO.Credentials;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Credentials.Commands.Change_Password
{
    public record ChangePasswordCommand(int userId , ChangePasswordRequest dto ): IRequest<ChangeCredentialsResult>;
}
