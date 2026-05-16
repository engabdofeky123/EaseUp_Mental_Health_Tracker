using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeRepository _homeRepository;
        

        public HomeController(IHomeRepository repo)
        {
            _homeRepository = repo;
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetHomeData()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userID").Value;
            int user_Id = int.Parse(userId);

            var result = await _homeRepository.GetHomeData(user_Id);
            if (!result.IsSuccess) 
                return BadRequest(result);
            return Ok(result);
        }
    }
}
