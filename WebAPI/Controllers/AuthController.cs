using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace arde_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto registerDto)
        {
            var userExistsResult = _authService.UserExists(registerDto.Email);
            if (!userExistsResult.Success)
            {
                return BadRequest(userExistsResult);
            }
            var registerResult = _authService.Register(registerDto);
            var tokenResult = _authService.CreateAccessToken(registerResult.Data);
            if (tokenResult.Success) return Ok(tokenResult);
            return BadRequest(tokenResult);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto loginDto)
        {
            var userToLogin = _authService.Login(loginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
