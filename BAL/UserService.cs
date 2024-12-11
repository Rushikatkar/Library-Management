using AutoMapper;
using DAL.Models.DTOs;
using DAL.Models.Entities;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => _mapper.Map<UserDTO>(user));
        }

        public async Task CreateUserAsync(UserRegistrationDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password); // Hash the password
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(int id, UserDTO userDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                _mapper.Map(userDto, user);
                await _userRepository.UpdateUserAsync(user);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<UserDTO> AuthenticateUserAsync(UserLoginDTO loginDto)
        {
            // Retrieve user by email
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
            {
                return null; // Invalid credentials
            }

            // Ensure the password is provided
            if (string.IsNullOrEmpty(loginDto.Password) || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return null; // Invalid credentials
            }

            // Generate JWT token with role claim
            var jwtToken = GenerateJwtToken(user);

            // Map and return authenticated user details along with the token
            var userDto = _mapper.Map<UserDTO>(user);
            userDto.JwtToken = jwtToken; // Add token to the returned DTO

            return userDto;
        }


        // Method to generate JWT token
        // Method to generate JWT token
        public string GenerateJwtToken(User user)
        {
            // Define claims, including the role claim
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("UserId", user.Id.ToString()), // Custom claim
        new Claim("Email", user.Email) // Custom claim
    };

            // Add role(s) to the claims
            if (!string.IsNullOrEmpty(user.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
            }

            // Get key from configuration and create signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Set token expiration
            var expiresIn = int.TryParse(_configuration["Jwt:ExpiresInMinutes"], out int expiresInMinutes)
                            ? expiresInMinutes : 60;

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
