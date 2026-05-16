using Application.DTO.Notifications;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;


namespace Application.Features.Notification.Commands
{
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand,Unit>
    {
        private readonly INotificationRepository _repo;
        private readonly INotificationService _service;

        public CreateNotificationHandler(INotificationRepository repo,INotificationService service)
        {
            _repo = repo;
            _service = service;
        }

        public async Task<Unit> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Message))
                throw new Exception("Invalid notification data");

            var notification = new NotificationDto
            {
                UserId = request.UserId,
                Title = request.Title,
                Message = request.Message,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(notification);

            try
            {
                await _service.SendToUser(notification);
            }
            catch
            {
                // log later
            }

            return Unit.Value;

        }
    }
}