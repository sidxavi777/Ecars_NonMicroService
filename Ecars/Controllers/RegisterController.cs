// RegisterController
using Ecars.Database.Repository;
using Ecars.Model;
using Ecars.Model.Dto_s;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Ecars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public RegisterController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var response = await userRepository.Register(request);
                if(response.IsSuccess == true) {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
                
            }
            else
            {
                return BadRequest(new ApiResponse
                {
                    ErrorMessages = new List<string> { "Please enter valid fields" },
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    result = null,
                    IsSuccess = false
                });
            }
        }
    }
}