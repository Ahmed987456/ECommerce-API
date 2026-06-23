using E_Commerce_API.Dtos.AuthDtos;
using E_Commerce_API.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// متاح للكل - تسجيل الدخول
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            var user = await _authService.Login(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password");
            var token = _jwtService.GenerateToken(user);
            return Ok(new
            {
                Token = token,
                User = new { user.Id, user.Name, user.Email, user.Role }
            });
        }
    }
}