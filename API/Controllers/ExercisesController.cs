using Application.Features.Exercises.Commands.Complete;
using Application.Features.Exercises.Queries.Get_All;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExercisesController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllExercises()
        {
            return Ok(await _mediator.Send(new GetAllExercisesQuery()));
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteExercise()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
            int user_Id = int.Parse(userId);

            var command = new CompleteExerciseCommand(user_Id);
            var result = await _mediator.Send(command);

            if(result == false)
                return NotFound(new {Message = "Can't find the student .. Invalid User Id"});
            return Ok(new { Message = "Exercise completed successfully" });
        }
    }
}
