using Application.DTO.Notifications;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public async Task SendToUser(NotificationDto notification)
        {
            await _hub.Clients
                .Group(notification.UserId.ToString())
                .SendAsync("ReceiveNotification", new
                {
                    notification.Title,
                    notification.Message,
                    notification.CreatedAt
                });
        }

    }
}
