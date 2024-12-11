using System;
using System.Threading.Tasks;
using BAL;
using DAL.Models.DTOs;
using DAL.Models.Entities; // User entity
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Applies authentication to all actions by default
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        /// <returns>List of users.</returns>
        // GET: api/User/GetAllUsers
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.LogInformation("Fetching all users.");
                var users = await _userService.GetAllUsersAsync();
                _logger.LogInformation("Successfully retrieved users.");
                return Ok(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving users.", error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieve a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>User details.</returns>
        // GET: api/User/GetUserById/{id}
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching user with ID {id}.");
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound(new { success = false, message = $"User with ID {id} not found." });
                }

                _logger.LogInformation($"Successfully retrieved user with ID {id}.");
                return Ok(new { success = true, data = user });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving user with ID {id}.");
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userDto">The user registration details.</param>
        /// <returns>Success message.</returns>
        // POST: api/User/RegisterUser
        [AllowAnonymous] // Allow anonymous access to registration
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDTO userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid input data for user registration.");
                    return BadRequest(new { success = false, message = "Invalid input data.", errors = ModelState });
                }

                _logger.LogInformation("Registering a new user.");
                await _userService.CreateUserAsync(userDto);
                _logger.LogInformation("User registered successfully.");
                return Ok(new { success = true, message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering a new user.");
                return StatusCode(500, new { success = false, message = "An error occurred while registering the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Authenticate a user and log them in.
        /// </summary>
        /// <param name="loginDto">The user login details.</param>
        /// <returns>Authenticated user details or an error message.</returns>
        // POST: api/User/Login
        [HttpPost("Login")]
        [AllowAnonymous] // Allow anonymous access to login
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid login data provided.");
                    return BadRequest(new { success = false, message = "Invalid login data.", errors = ModelState });
                }

                _logger.LogInformation("Attempting user login.");
                var userDto = await _userService.AuthenticateUserAsync(loginDto); // Returns UserDTO with JWT

                if (userDto == null)
                {
                    _logger.LogWarning("Invalid email or password.");
                    return Unauthorized(new { success = false, message = "Invalid email or password." });
                }

                _logger.LogInformation("User logged in successfully.");
                return Ok(new { success = true, message = "Login successful.", token = userDto.JwtToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user login.");
                return StatusCode(500, new { success = false, message = "An error occurred during login.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The updated user details.</param>
        /// <returns>Success message.</returns>
        // PUT: api/User/UpdateUser/{id}
        [Authorize(Roles = "User,Admin")]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDTO userUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Updating user with ID {id}.");

                var userExists = await _userService.GetUserByIdAsync(id);
                if (userExists == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound(new { success = false, message = $"User with ID {id} not found." });
                }

                userExists.UserName = userUpdateDto.UserName;
                userExists.Email = userUpdateDto.Email;
                userExists.Role = userUpdateDto.Role;
                userExists.IsActive = userUpdateDto.IsActive;
                userExists.LastLogin = userExists.LastLogin ?? DateTime.UtcNow;

                await _userService.UpdateUserAsync(id, userExists);

                _logger.LogInformation($"User with ID {id} updated successfully.");
                return Ok(new { success = true, message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating user with ID {id}.");
                return StatusCode(500, new { success = false, message = "An error occurred while updating the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Success message.</returns>
        // DELETE: api/User/DeleteUser/{id}
        [Authorize(Roles = "User,Admin")]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting user with ID {id}.");

                var userExists = await _userService.GetUserByIdAsync(id);
                if (userExists == null)
                {
                    _logger.LogWarning($"User with ID {id} not found.");
                    return NotFound(new { success = false, message = $"User with ID {id} not found." });
                }

                await _userService.DeleteUserAsync(id);
                _logger.LogInformation($"User with ID {id} deleted successfully.");
                return Ok(new { success = true, message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting user with ID {id}.");
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the user.", error = ex.Message });
            }
        }
    }
}
