using Application.DTO.Message;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Commands
{
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, MessageDto>
    {
        private readonly IMessageRepository _repo;
        private readonly IChatService _chatService;

        public SendMessageHandler(IMessageRepository repo ,IChatService chatService)
        {
            _repo = repo;
            _chatService = chatService;
        }

        public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new MessageDto
            {
                UserId = request.UserId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            await _repo.AddNewMsg(message);

            // 🔥 send realtime
            await _chatService.BroadcastMessage(message);

            return message;
        }
    }
}
