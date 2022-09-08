using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginwithauthentication.Models;
using loginwithauthentication.Data;
using loginwithauthentication.Dtos.UserDto;

namespace loginwithauthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {
            ServiceResponse<int> serviceResponse = await _authRepository.Register
            (new User{ Username = request.Username }, request.Password);
            if(serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse);
            }
            else
                return Ok(serviceResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            ServiceResponse<string> serviceResponse = await _authRepository.Login
            ( request.Username , request.Password);
            if(serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse);
            }
            else
                return Ok(serviceResponse);
        }
    }
}