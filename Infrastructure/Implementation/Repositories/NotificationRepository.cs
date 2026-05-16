using Application.DTO.Notifications;
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
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(NotificationDto notification)
        {
            var notificatinModel = new Notification
            {
                Content = notification.Message,
                IsRead = notification.IsRead,
                SentAt = notification.CreatedAt,
                Title = notification.Title,
                UserId = notification.UserId
            };
            await _context.Notifications.AddAsync(notificatinModel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.SentAt)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification is null) return;

            notification.IsRead = true;

            await _context.SaveChangesAsync();
        }

    }
}
