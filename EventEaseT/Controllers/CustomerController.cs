// Controllers/CustomerController.cs
using Microsoft.AspNetCore.Mvc;

namespace EventEaseT.Controllers
{
    // CustomerController handles requests related to the Customer area of the application.
    // This is where customer-specific views and actions will be defined.
    public class CustomerController : Controller
    {
        // GET: Customer/Index
        // This action method returns the default Customer dashboard view.
        // Currently, it just loads the Index.cshtml view under Views/Customer.
        public IActionResult Index()
        {
            return View();
        }
    }
}