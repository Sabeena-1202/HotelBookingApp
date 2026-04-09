using HotelAuthService.DTOs;
using HotelAuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelAuthService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDto registerDto)
        {
            _logger.LogInformation(
                "Register attempt for email: {Email}",
                registerDto.Email);

            var result = await _authService.RegisterAsync(registerDto);

            _logger.LogInformation(
                "Registration successful for email: {Email}",
                registerDto.Email);

            return Ok(new
            {
                message = result
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginDto loginDto)
        {
            _logger.LogInformation(
                "Login attempt for email: {Email}",
                loginDto.Email);

            var token = await _authService.LoginAsync(loginDto);

            _logger.LogInformation(
                "Login successful for email: {Email}",
                loginDto.Email);

            return Ok(new
            {
                token = token
            });
        }
    }
}