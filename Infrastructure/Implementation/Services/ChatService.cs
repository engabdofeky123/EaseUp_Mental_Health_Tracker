using Application.DTO.Message;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Infrastructure.SignalR;

namespace Infrastructure.Implementation.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _hub;

        public ChatService(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        public async Task BroadcastMessage(MessageDto message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", new
            {
                message.UserId,
                message.Content,
                message.SentAt
            });
        }
    }
}

