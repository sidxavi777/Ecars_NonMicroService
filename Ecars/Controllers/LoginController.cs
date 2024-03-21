using Ecars.Database.Repository;
using Ecars.Model.Dto_s;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public LoginController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {

            var response = await userRepository.Login(request);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}