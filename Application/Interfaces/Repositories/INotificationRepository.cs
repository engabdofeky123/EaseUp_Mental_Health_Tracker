using Application.DTO.Notifications;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(NotificationDto notification);
        Task<List<Notification>> GetByUserIdAsync(int userId);
        Task MarkAsReadAsync(int id);
    }
}
