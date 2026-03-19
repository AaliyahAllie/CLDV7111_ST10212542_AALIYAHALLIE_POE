// Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    // AdminController handles requests related to the Admin area of the application.
    // This is where admin-specific views and actions will be defined.
    public class AdminController : Controller
    {
        // GET: Admin/Index
        // This action method returns the default Admin dashboard view.
        // Currently, it just loads the Index.cshtml view under Views/Admin.
        public IActionResult Index()
        {
            return View();
        }
    }
}