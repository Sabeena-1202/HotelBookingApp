using HotelAuthService.DTOs;
using HotelAuthService.Models;
using HotelAuthService.Repositories;

namespace HotelAuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var emailExists = await _userRepository
                .EmailExistsAsync(registerDto.Email);

            if (emailExists)
            {
                throw new Exception("Email already registered");
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                    registerDto.Password),
                Role = "User"
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return "Registration successful";
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository
                .GetByEmailAsync(loginDto.Email);

            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            var isPasswordValid = BCrypt.Net.BCrypt
                .Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password");
            }

            return _tokenService.GenerateToken(user);
        }
    }
}