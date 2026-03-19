using EventEaseT.Data;                   // DbContext for EF Core
using EventEaseT.Models;                 // User and Role models
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;      // For password hashing
using System.Text;                       // For encoding password input

public class AccountController : Controller
{
    private readonly EventEaseContext _context;

    // Constructor injects the database context
    public AccountController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Register page
    public IActionResult Register() => View();

    // GET: Admin dashboard view
    public IActionResult AdminDash() => View();

    // POST: Register a new user
    [HttpPost]
    public IActionResult Register(string name, string email, string username, string password, string role)
    {
        // Check if username already exists
        if (_context.Users.Any(u => u.Username == username))
        {
            ViewBag.Error = "Username already exists.";
            return View();
        }

        // Check if email already exists
        if (_context.Users.Any(u => u.Email == email))
        {
            ViewBag.Error = "Email already exists.";
            return View();
        }

        // Get role ID from Roles table
        var selectedRole = _context.Roles.First(r => r.RoleName == role);

        // Create new user object
        var newUser = new User
        {
            Name = name,
            Email = email,
            Username = username,
            PasswordHash = HashPassword(password),
            RoleId = selectedRole.RoleId,
            CreatedAt = DateTime.Now
        };

        // Save user to database
        _context.Users.Add(newUser);
        _context.SaveChanges();

        // Redirect to login after successful registration
        return RedirectToAction("Login");
    }

    // GET: Login page
    public IActionResult Login() => View();

    // POST: Authenticate user and redirect based on role
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var hashedPassword = HashPassword(password);

        // Find user with matching credentials
        var user = _context.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

        if (user != null)
        {
            // Redirect based on role
            if (user.Role.RoleName == "Admin")
            {
                return RedirectToAction("AdminDash", "Account");
            }
            else if (user.Role.RoleName == "Customer")
            {
                return RedirectToAction("CustomerDash", "CustomerDashboard");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Invalid login
        ViewBag.Error = "Invalid credentials.";
        return View();
    }

    // POST: Logout user and clear session
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();           // Clear session variables
        HttpContext.SignOutAsync();            // Clear authentication cookies
        return RedirectToAction("Login", "Account");
    }

    // Helper method to hash passwords using SHA256
    private string HashPassword(string password)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes); // Base64 encoding for storage
        }
    }
}