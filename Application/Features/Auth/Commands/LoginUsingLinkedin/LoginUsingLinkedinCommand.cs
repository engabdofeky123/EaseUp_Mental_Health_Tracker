using Application.DTO.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.LoginUsingLinkedin
{
    public record LoginUsingLinkedinCommand(string code):IRequest<LinkedinTokenResponse>;
}
