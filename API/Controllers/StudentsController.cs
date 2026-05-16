using Application.DTO.Student;
using Application.Features.Student.Commands.Add_Mood_Journaling;
using Application.Features.Student.Queries.GetDashBoard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediatR) 
        {
            _mediator = mediatR;
        }

        [Authorize(Roles="student")]
        [HttpPost("add-mood-journal")]
        public async Task<IActionResult> AddMoodJournal([FromBody] AddJournalMoodInputDto moodInput, CancellationToken cancellationToken)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID");
            int user_id = int.Parse(userId.Value);

            var command = new AddJournalMoodCommand(user_id,moodInput);
            var result = await _mediator.Send(command, cancellationToken: cancellationToken);
            if (result.isSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userID");
            if (userIdClaim is null)
                return Unauthorized(new { Message = "Invalid token: userID claim is missing." });

            int userId = int.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new GetDashboardQuery(userId), cancellationToken);

            if (result is null)
                return NotFound(new { Message = "Student not found or not registered." });

            return Ok(result);
        }
    }
}