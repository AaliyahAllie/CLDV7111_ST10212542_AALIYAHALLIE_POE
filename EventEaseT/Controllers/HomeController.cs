using System.Diagnostics;                // For Activity tracing in error handling
using EventEaseT.Models;                 // ErrorViewModel for error reporting
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    // HomeController manages general site pages such as the landing page,
    // privacy policy, and error handling.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor injects a logger for diagnostic and error tracking
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home/Index
        // Loads the default landing page (Index.cshtml).
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/Privacy
        // Loads the privacy policy page (Privacy.cshtml).
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Error
        // Handles application errors and displays an error view.
        // ResponseCache is disabled to ensure fresh error details are always shown.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Passes an ErrorViewModel with the current request ID for troubleshooting
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}