
// Controllers/CustomerController.cs
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}