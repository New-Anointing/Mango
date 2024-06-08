using Mango.Services.AuthAPI.Models.DTOs;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto response;
        public AuthAPIController(IAuthService authService)
        {
            _authService=authService;
            response=new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequestDto request)
        {
            var errorMessage = await _authService.Register(request);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.IsSuccess = false;
                response.Message = errorMessage;
                return BadRequest(response);
            }

            return Ok(response);
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto request)
        {
            var result = await _authService.Login(request);
            if(result.User is null)
            {
                response.IsSuccess = false;
                response.Message = "Username or password is incorrect";
                return BadRequest(response);
            }
            response.Data = result;
            return Ok(response);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody]RegistrationRequestDto request)
        {
            var result = await _authService.AssignRole(request.Email, request.RoleName.ToUpper());
            if (!result)
            {
                response.IsSuccess = false;
                response.Message = "Eroor encountered";
                return BadRequest(response);
            }
            response.Data = result;
            return Ok(response);
        }
    }
}
