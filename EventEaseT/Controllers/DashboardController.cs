// Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDash() => Content("Admin Dashboard - Account Logged In");
        public IActionResult CustomerDash() => Content("Customer Dashboard - Account Logged In");
    }
}