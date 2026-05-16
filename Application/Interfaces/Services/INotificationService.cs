using Application.DTO.Notifications;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendToUser(NotificationDto notification);
    }
}
