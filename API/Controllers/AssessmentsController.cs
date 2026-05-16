using Microsoft.AspNetCore.Mvc;
using Application.DTO.Mentals;
using Application.Features.Assessment.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentsController : ControllerBase 
    {
        private readonly IMediator _mediator;

        public AssessmentsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("save-results")]
        public async Task<IActionResult> SaveResults([FromBody] MentalInput input)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
            int user_Id = int.Parse(userId);

            var command = new CreateAssessmentCommand(user_Id,input.DepressionScore,input.AnxietyScore,input.StressScore,input.Diagnosis);

            var result = await _mediator.Send(command);

            return (result == null)? BadRequest(new { Message = "No Data Retreived"}) : Ok(result);
        }
    }
}
