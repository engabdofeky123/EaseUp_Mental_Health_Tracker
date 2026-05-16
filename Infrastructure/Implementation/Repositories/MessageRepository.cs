using Application.DTO.Message;
using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewMsg(MessageDto message) 
        {
            var msg = new ChatGroupMessage
            {
                Content = message.Content,
                SentAt = message.SentAt,
                UserId = message.UserId,
                UserName = message.UserName
            };
            await _context.ChatGroups.AddAsync(msg);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MessageDto>> GetNewMsgList()
        {
            return await _context.ChatGroups.Select(m => new MessageDto
            {
                Content = m.Content,
                SentAt = m.SentAt,
                UserId = m.UserId,
                UserName = m.UserName
                
            }).OrderBy(x => x.SentAt).ToListAsync();
        }

    }
}
