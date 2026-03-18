using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;  

namespace EventEaseT.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }      // NEW
        public string Email { get; set; }     // NEW
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
