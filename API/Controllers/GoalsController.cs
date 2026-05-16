using Application.DTO.Goals;
using Application.Features.Student.Commands.Add_New_Goal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Goals.Queries.GetAllActiveGoals;
using Application.Features.Goals.Queries.Get_Goal_By_Id;
using Application.Features.Goals.Commands.Delete_Goal;
using Application.Features.Goals.Commands.Update_Goal;
using Application.Features.Goals.Commands.Toggle_Item;
using Application.Interfaces.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IGoalsRepository _goalsRepo;

        public GoalsController(IMediator mediatR, IGoalsRepository repo) 
        {
            _mediator = mediatR;
            _goalsRepo = repo;
        }

        //[Authorize(Roles = "student")]
        [HttpGet("active-goals")]
        public async Task<IActionResult> GetActiveGoals()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID").Value;
            int user_id = int.Parse(userId);

            var query = new GetAllActiveGoalsQuery(user_id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        //[Authorize(Roles = "student")]
        [HttpPost("add-new-goal")]
        public async Task<IActionResult> AddNewGoal(AddNewGoalInputDto dto, CancellationToken cancellationToken)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID");
            int user_id = int.Parse(userId.Value);

            var command = new AddNewGoalCommand(user_id, dto);
            var result = await _mediator.Send(command);

            if (result.isSuccess == false)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetGoalById(int id)
        {
            var query = new GetGoalByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result == null)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet("all-goals/{studentId:int}")]
        public async Task<IActionResult> GetAllGoals(int studentId)
        {
            var goals =  await _goalsRepo.GetAllGoalsByStudentId(studentId);
            if (goals == null || goals.Count <= 0 )
                return NotFound();
            return Ok(goals);
        }


        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteGoalById(int id)
        {
            var command = new DeleteGoalCommand(id);
            var result = await _mediator.Send(command);
            if(result == false)
                return NotFound(new {Message = "Can not delete the goal"});
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateGoal([FromQuery] int goalId , [FromBody] UpdateGoalDto dto)
        {
            var command = new UpdateGoalCommand(goalId, dto);
            var result = await _mediator.Send(command);
            if(result == false)
                return NotFound(new { Message = "Can not delete the goal" });
            return Ok(new { Message = "Update successfully" });
        }

        [HttpPost("toggle-item")]
        public async Task<IActionResult> ToggleItem(ToggleItemsDto dto )
        {
            var command = new ToggleItemCommand(dto.itemId, dto.studentId, dto.goalId);
            var result = await _mediator.Send(command);

            if (result == false)
                return NotFound(new { Message = "Can not delete the goal" });
            return Ok(new { Message = "Toggled successfully" });
        }
    }
}