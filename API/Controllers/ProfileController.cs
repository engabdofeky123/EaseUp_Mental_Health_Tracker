using Application.DTO.Credentials;
using Application.DTO.Profile;
using Application.DTO.Student;
using Application.Features.Credentials.Commands.Change_Email;
using Application.Features.Credentials.Commands.Change_Password;
using Application.Features.Profile.Commands.UpdateStudentProfile;
using Application.Features.Profile.Commands.UpdateSupervisorProfile;
using Application.Features.Profile.Queries.GetStudentProfile;
using Application.Features.Profile.Queries.GetSupervisorProfile;
using Application.Interfaces.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase 
    {
        private readonly IMediator _mediator;
        private readonly IGetStudentByUserIdService _getStudentByUserId;

        public ProfileController(IMediator mediator, IGetStudentByUserIdService service)
        {
            _mediator = mediator;
            _getStudentByUserId = service;
        }

        [HttpGet]
        [Route("student/profile")]
        public async Task<IActionResult> ViewStudentProfile([FromQuery] int studentId)
        {
            var query = new GetStudentProfileQuery(studentId);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        [Route("supervisor/profile")]
        public async Task<IActionResult> ViewSupervisorProfile([FromQuery] int supervisorId)
        {
            var query = new GetSupervisorProfileQuery(supervisorId);
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut]
        [Route("update/student-profille/")]
        public async Task<IActionResult> UpdateStudentProfile([FromForm]  UpdateStudentDto student)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "userID").Value;
            int user_id = int.Parse(userId);

            var cmd = new UpdateStudentProfileCommand(user_id, student);
            var result = await _mediator.Send(cmd);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut]
        [Route("update/supervisor/{supervisorId}")]
        public async Task<IActionResult> UpdateSupervisorProfile([FromRoute] int supervisorId, Supervisor supervisor)
        {
            var cmd = new UpdateSupervisorProfileCommand(supervisorId, supervisor);
            var result = await _mediator.Send(cmd);

            return result == null ? NotFound() : Ok(result);
        }
        
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
            int user_Id = int.Parse(userId);

            var cmd = new ChangePasswordCommand(user_Id,request);
            var result = await _mediator.Send(cmd);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Route("change-email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
            int user_Id = int.Parse(userId);

            var cmd = new ChangeEmailCommand(user_Id, request);
            var result = await _mediator.Send(cmd);

            return result == null ? NotFound() : Ok(result);
        }

        //[HttpPost("upload-image")]
        //public async Task<IActionResult> UploadProfileImage(IFormFile file)
        //{

        //    var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
        //    int user_Id = int.Parse(userId);

        //    try
        //    {
        //        // جلب userId من الـ Token
        //        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userID");
        //        if (userIdClaim == null)
        //        {
        //            return Unauthorized(new { message = "User not authenticated" });
        //        }


        //        // جلب الطالب باستخدام userId
        //        var student = await _getStudentByUserId.GetStudentAsyncByUserID(user_Id,cancellationToken: new CancellationToken());
        //        if (student == null)
        //        {
        //            return NotFound(new { message = "Student not found" });
        //        }

        //        if (file == null || file.Length == 0)
        //        {
        //            return BadRequest(new { message = "No file uploaded" });
        //        }

        //        // تحديد مجلد حفظ الصور
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        //        if (!Directory.Exists(uploadsFolder))
        //        {
        //            Directory.CreateDirectory(uploadsFolder);
        //        }

        //        // إنشاء اسم فريد للملف
        //        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        //        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        // حفظ الملف
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        var imageUrl = $"/uploads/{uniqueFileName}";

        //        // تحديث قاعدة البيانات
        //        student.ProfilePictureUrl = imageUrl;
        //        await _mediator.Send(new UpdateStudentProfileCommand(user_Id, student));

        //        // إعادة استجابة JSON صحيحة
        //        return Ok(new
        //        {
        //            message = "Image uploaded successfully",
        //            imageUrl = imageUrl
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
        //    }
        //}

    }
}