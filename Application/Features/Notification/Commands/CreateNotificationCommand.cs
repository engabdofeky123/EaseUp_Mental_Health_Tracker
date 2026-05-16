using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notification.Commands
{
    public record CreateNotificationCommand(int UserId,string Title,string Message) : IRequest<Unit>;
}
