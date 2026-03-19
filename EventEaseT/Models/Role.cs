using System.ComponentModel.DataAnnotations;

namespace EventEaseT.Models
{
    // Role represents a user role in the system (e.g., Admin, Customer).
    // It is linked to Users to enforce role-based authorization.
    public class Role
    {
        // Primary key for the Role table
        [Key]
        public int RoleId { get; set; }

        // Name of the role (must be unique, e.g., "Admin", "Customer")
        [Required]
        public string RoleName { get; set; }

        // Navigation property: collection of users assigned to this role
        public ICollection<User> Users { get; set; }
    }
}