using EventEaseT.Data;       // Your DbContext namespace
using EventEaseT.Data;
using EventEaseT.Models;    // Your User/Role models namespace
using EventEaseT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using System.Text;

public class AccountController : Controller
{
    private readonly EventEaseContext _context;

    public AccountController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Register
    public IActionResult Register() => View();

    public IActionResult AdminDash() => View();

    [HttpPost]
    public IActionResult Register(string name, string email, string username, string password, string role)
    {
        if (_context.Users.Any(u => u.Username == username))
        {
            ViewBag.Error = "Username already exists.";
            return View();
        }

        if (_context.Users.Any(u => u.Email == email))
        {
            ViewBag.Error = "Email already exists.";
            return View();
        }

        var selectedRole = _context.Roles.First(r => r.RoleName == role);

        var newUser = new User
        {
            Name = name,
            Email = email,
            Username = username,
            PasswordHash = HashPassword(password),
            RoleId = selectedRole.RoleId,
            CreatedAt = DateTime.Now
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }

    // GET: Login
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var hashedPassword = HashPassword(password);

        var user = _context.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

        if (user != null)
        {
            if (user.Role.RoleName == "Admin")
            {
                return RedirectToAction("AdminDash", "Account");
            }
            else if (user.Role.RoleName == "Customer")
            {
                // Redirect to CustomerDash.cshtml under Account/CustomerDashboard
                return RedirectToAction("CustomerDash", "CustomerDashboard");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        ViewBag.Error = "Invalid credentials.";
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        // Clear session
        HttpContext.Session.Clear();

        // Clear authentication cookie if you’re using Identity
        HttpContext.SignOutAsync();

        // Redirect to login page
        return RedirectToAction("Login", "Account");
    }


    private string HashPassword(string password)
    {
        using (var sha = SHA256.Create())
        {
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes); // Base64 for consistency
        }
    }
}
