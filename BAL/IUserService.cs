using DAL.Models.DTOs;
using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task CreateUserAsync(UserRegistrationDTO userDto);
        Task UpdateUserAsync(int id, UserDTO userDto);
        Task DeleteUserAsync(int id);
        Task<UserDTO> AuthenticateUserAsync(UserLoginDTO loginDto); // New method for login
        string GenerateJwtToken(User user); // Add this method to the interface
    }

}
