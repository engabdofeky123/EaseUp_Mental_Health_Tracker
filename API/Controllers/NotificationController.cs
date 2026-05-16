using Application.DTO.Notifications;
using Application.Features.Notification.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase 
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpPost]
        public async Task<IActionResult> PostNotification([FromBody] NotificationInputs dto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userID");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            var command = new CreateNotificationCommand(userId, dto.Title, dto.Message);

            await _mediator.Send(command);

            return Ok(new { message = "Notification sent successfully" });
        }
    }
}
