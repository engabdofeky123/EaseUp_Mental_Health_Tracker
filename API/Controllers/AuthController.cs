using Application.Features.Student.Commands.RegisterNewStudent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _config;
        public AuthController(IMediator mediatR, IConfiguration options)
        {
            _mediator = mediatR;
            _config = options;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Application.DTO.Authentication.RegisterDto dto)
        {
            var command = new RegisterNewStudentCommand(dto);
            var result = await _mediator.Send(command);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Application.DTO.Authentication.LoginDto dto)
        {
            var command = new Application.Features.Auth.Commands.Login.LoginCommand(dto);
            var result = await _mediator.Send(command);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }

        // Google Login

        [HttpGet("redirect-to-google")]
        public IActionResult RedirectToGoogle()
        {
            var clientId = _config["Google:ClientId"];
            var redirectUri = _config["Google:RedirectUri"];
            var state = Guid.NewGuid().ToString(); // Generate a random state value for security
            var scope = "openid profile email";
            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&state={state}&scope={Uri.EscapeDataString(scope)}";
            return Redirect(googleAuthUrl);
        }


        [HttpPost("google-callback")]
        public async Task<IActionResult> LoginUsingGoogle( string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Authorization code not found");

            var command = new Application.Features.Auth.Commands.LoginUsingGoogle.LoginUsingGoogleCommand(code);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Google authentication failed.");
            return Ok(result);
        }


        // LinkedIn login 

        [HttpGet("redirect-to-linkedin")]
        public IActionResult RedirectToLinkedin()
        {
            var clientId = _config["LinkedIn:ClientId"];
            var redirectUri = _config["LinkedIn:RedirectUri"];
            var state = Guid.NewGuid().ToString(); // Generate a random state value for security
            var scope = "openid profile email";
            var linkedinAuthUrl = $"https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&state={state}&scope={Uri.EscapeDataString(scope)}";
            return Redirect(linkedinAuthUrl);
        }

        [HttpPost("linkedin-callback")]
        public async Task<IActionResult> LoginUsingLinkedin([FromBody] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Authorization code not found");

            var command = new Application.Features.Auth.Commands.LoginUsingLinkedin.LoginUsingLinkedinCommand(code);
            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }
    }
}