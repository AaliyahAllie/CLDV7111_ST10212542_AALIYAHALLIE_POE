// Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}