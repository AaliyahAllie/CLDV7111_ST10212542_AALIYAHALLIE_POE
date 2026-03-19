
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    // DashboardController provides role-based dashboard views.
    // It acts as a simple entry point for Admin and Customer dashboards.
    public class DashboardController : Controller
    {
        // GET: Dashboard/AdminDash
        // Returns a placeholder content message for the Admin dashboard.
        // In a full implementation, this would load Views/Dashboard/AdminDash.cshtml
        // and display admin-specific features (manage venues, events, bookings).
        public IActionResult AdminDash() => Content("Admin Dashboard - Account Logged In");

        // GET: Dashboard/CustomerDash
        // Returns a placeholder content message for the Customer dashboard.
        // In a full implementation, this would load Views/Dashboard/CustomerDash.cshtml
        // and display customer-specific features (browse events, create bookings).
        public IActionResult CustomerDash() => Content("Customer Dashboard - Account Logged In");
    }
}