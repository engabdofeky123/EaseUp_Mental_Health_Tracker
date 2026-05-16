using Application.Features.Gamification.Queries.Get_Gamification;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamificationController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetGamification()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID");
            int user_id = int.Parse(userId.Value);

            var query = new GetGamificationQuer(user_id);
            var result = await _mediator.Send(query);

            if(result == null)
                return NotFound(new { Message = "Can't Find the student maybe invalid ID"});
            return Ok(result);
        }
    }
}