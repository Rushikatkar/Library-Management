using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string PasswordHash { get; set; } // Store hashed password here

        public string Role { get; set; } // Can be "Admin" or "User"

        public bool IsActive { get; set; } = true; // User is active by default
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Auto-set on creation
        public DateTime? LastLogin { get; set; } // Nullable to track user's last login
    }
}
