using Application.DTO.AiModelDto;
using Application.Features.AiSurvey.Commands;
using Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IMediator mediator;

        public SurveyController(IMediator service) 
        {
            mediator = service;    
        }

        // Save the AI Results In DB Endpoint

        [HttpPost("/save-responses")]
        public async Task<IActionResult> SaveResponsesInDB([FromBody] AiSurveyInputDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
            int user_Id = int.Parse(userId);

            var commmand = new SaveSurveyResponsesInDbCommand(user_Id, dto) ;
            var result = await mediator.Send(commmand);

            if (result == null)
                return Unauthorized();

            if (!result.IsSaved)
                return BadRequest(result);
            return Ok(result);
        }

    }
}
