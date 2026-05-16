using Application.DTO.Message;
using Application.Features.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MessageInputDto contenet)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID");
            int user_id = int.Parse(userId.Value);

            var command = new SendMessageCommand(user_id, contenet.content);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
