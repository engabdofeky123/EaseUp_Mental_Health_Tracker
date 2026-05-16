using Application.DTO.Message;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Commands
{
    public record SendMessageCommand(int UserId, string Content): IRequest<MessageDto>;
}