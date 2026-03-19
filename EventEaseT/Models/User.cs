using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace EventEaseT.Models
{
    // User represents an account in the EventEase system.
    // It stores authentication details and links to a Role for authorization.
    public class User
    {
        // Primary key for the User table
        public int UserId { get; set; }

        // Full name of the user (customer or admin)
        public string Name { get; set; }

        // Email address of the user (should be unique)
        public string Email { get; set; }

        // Username for login (must be unique)
        public string Username { get; set; }

        // Hashed password for secure authentication
        public string PasswordHash { get; set; }

        // Foreign key linking to the Role table
        public int RoleId { get; set; }

        // Navigation property for the related Role
        public Role Role { get; set; }

        // Date and time when the account was created
        public DateTime CreatedAt { get; set; }
    }
}