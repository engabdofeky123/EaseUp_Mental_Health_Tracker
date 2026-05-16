using Application.DTO.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.LoginUsingGoogle
{
    public record LoginUsingGoogleCommand(string code): IRequest<GoogleTokenResponse>;
}
