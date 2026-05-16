using Application.DTO.Credentials;
using Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Credentials.Commands.Change_Password
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ChangeCredentialsResult>
    {
        private readonly IChangeCredientials credientials;

        public ChangePasswordHandler(IChangeCredientials creds)
        {
            credientials = creds;  
        }
        public async Task<ChangeCredentialsResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrEmpty(request.dto.CurrentPassword) || string.IsNullOrEmpty(request.dto.NewPassword))
                return new ChangeCredentialsResult { Message = "No Email sent"! };

            return await credientials.ChangePasswordAsync(request.userId, request.dto);
        }
    }
}
