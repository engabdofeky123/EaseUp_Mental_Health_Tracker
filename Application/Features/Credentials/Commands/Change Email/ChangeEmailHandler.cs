using Application.DTO.Credentials;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Credentials.Commands.Change_Email
{
    public class ChangeEmailHandler : IRequestHandler<ChangeEmailCommand, ChangeCredentialsResult>
    {
        private readonly IChangeCredientials _changeCreds;
        public ChangeEmailHandler(IChangeCredientials creds)
        {
            _changeCreds = creds;
        }
        public async Task<ChangeCredentialsResult> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            if(request == null||string.IsNullOrEmpty(request.dto.NewEmail)) 
                return new ChangeCredentialsResult { Message = "No Email sent" !};
            return await _changeCreds.ChangeEmailAsync(request.userId, request.dto);
        }
    }
}
