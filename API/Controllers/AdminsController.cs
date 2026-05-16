using Application.DTO.Admins;
using Application.Features.Admin.AddNewAdmin;
using Application.Features.Admin.Commands.Update_student_Notes;
using Application.Features.Admin.Commands.UpdateProfileInformation;
using Application.Features.Admin.Queries.AdminManagementInformation;
using Application.Features.Admin.Queries.MonitoringList;
using Application.Features.Admin.Queries.Overview;
using Application.Features.Admin.Queries.StudentProfileInformation;
using Application.Features.Admin.Queries.ViewProfileInformation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AdminsController(IMediator mediatR)
        {
            _mediator = mediatR;
        }

        [HttpGet("overview")]
        public async Task<IActionResult> Overview(CancellationToken cancellationToken)
        {
            var query = new AdminOverviewQuery();
            var result = await _mediator.Send(query, cancellationToken: cancellationToken);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("user-monitoring")]
        public async Task<IActionResult> UserMonitoring(CancellationToken cancellationToken)
        {
            var query = new MonitoringListQuery();
            var result = await _mediator.Send(query, cancellationToken: cancellationToken);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("student-profile-information")]
        public async Task<IActionResult> GetStudentProfileById([FromQuery] int studentId , CancellationToken cancellationToken)
        {
            var query = new ViewStudentProfileInformationQuery(studentId);
            var result = await _mediator.Send(query, cancellationToken);

            return result == null ? NotFound(result) : Ok(result);
        }

        
        [HttpPut("update-current-note")]
        public async Task<IActionResult> UpdateCurrentNote(int studentId , string updated_Note)
        {
            var command = new UpdateStudentNoteCommand(studentId, updated_Note);
            var result = await _mediator.Send(command);

            return result == false ? BadRequest(result) : Ok(result);
        }

        [HttpGet("all-admins")]
        public async Task<IActionResult> GetAllAndAdmins(CancellationToken cancellationToken)
        {
            var query = new AdminManagementQuery();
            var result = await _mediator.Send(query,cancellationToken);

            if (result is null )
                return NotFound();
            return Ok(result);
        }

        [HttpPost("add-admin")]
        public async Task<IActionResult> AddNewAdmin(AddNewAdminDto dto)
        {
            var command = new AddNewAdminCommand(dto);
            var result = await _mediator.Send(command);
            if(!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("profile-information")]
        public async Task<IActionResult> GetProfileInformation()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID");
            int user_id = int.Parse(userId.Value);

            var query = new ViewProfileInformationQuery(user_id);
            var result = await _mediator.Send(query);
            if(result is null )
                return NotFound(result);
            return Ok(result);
        }

        [Authorize(Roles = "admin,supervisor")]
        [HttpPut("update-profile-information")]
        public async Task<IActionResult> UpdateProfileInformation( [FromForm] UpdateProfileInformationData dto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID");
            int user_id = int.Parse(userId.Value);

            var updateCmd = new UpdateProfileInformationCommand( user_id , dto);
            var result = await _mediator.Send(updateCmd);
            if(result is null )
                return NotFound(result);
            if(result.Errors.Count >0)
                return BadRequest(result);
            return Ok(result);
        }
    }
}