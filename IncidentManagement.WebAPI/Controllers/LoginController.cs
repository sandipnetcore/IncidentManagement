using IncidentManagement.BusinessLogic.User;
using IncidentManagement.DataModel.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Post([FromBody] LoginCredentialModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await _userRepository.GenerateToken(loginModel);

            if (string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(new { jsonToken = token });
        }


    }
}
