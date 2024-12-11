using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; } // Can be "Admin" or "User"

        public bool IsActive { get; set; } // Indicates if the user is active or disabled
        public DateTime CreatedAt { get; set; } // Tracks when the user was created
        public DateTime? LastLogin { get; set; } // Last login timestamp

        // Add JwtToken to return the generated JWT token
        public string JwtToken { get; set; } // JWT token for the authenticated user

        // Constructor to initialize the DTO with basic user details and token
        public UserDTO(int id, string userName, string email, string role, bool isActive, DateTime createdAt, DateTime? lastLogin, string jwtToken = null)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Role = role;
            IsActive = isActive;
            CreatedAt = createdAt;
            LastLogin = lastLogin;
            JwtToken = jwtToken; // Set the JwtToken if provided
        }
    }

    // DTO for user registration
    public class UserRegistrationDTO
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; } // Plain-text password for registration

        public string Role { get; set; } = "User"; // Default role is "User"
    }

    // DTO for user login (only email and password needed)
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress] // Use EmailAddress attribute to validate email format
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
    }

    // DTO for updating user details without JwtToken
    public class UserUpdateDTO
    {
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; } // Can be "Admin" or "User"

        public bool IsActive { get; set; } // Indicates if the user is active or disabled

        public DateTime? LastLogin { get; set; } // Last login timestamp (optional during update)
    }
}
