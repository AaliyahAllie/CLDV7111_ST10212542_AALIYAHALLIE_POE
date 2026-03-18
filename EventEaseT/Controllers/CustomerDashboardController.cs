using EventEaseT.Data;
using EventEaseT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventEaseT.Controllers.Account
{
    public class CustomerDashboardController : Controller
    {
        private readonly EventEaseContext _context;

        public CustomerDashboardController(EventEaseContext context)
        {
            _context = context;
        }

        // GET: /Account/CustomerDashboard/CustomerDash
        public IActionResult CustomerDash()
        {
            var userId = GetLoggedInUserId();

            var bookings = _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .Where(b => b.UserId == userId)
                .ToList();

            return View(bookings);
        }

        // GET: Account/CustomerDashboard/CreateBooking
        [HttpGet]
        public IActionResult CreateBooking(int? venueId)
        {
            ViewBag.Events = _context.Events.ToList();
            ViewBag.Venues = _context.Venues.ToList();

            var booking = new Booking();
            if (venueId.HasValue)
            {
                booking.VenueId = venueId.Value; // pre-select venue
            }

            return View(booking);
        }

        // POST: Account/CustomerDashboard/CreateBooking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking)
        {
            booking.UserId = GetLoggedInUserId();
            // BookingDate now comes from the form input
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("CustomerDash");
        }

        // GET: Account/CustomerDashboard/EditBooking/5
        public IActionResult EditBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        // POST: Account/CustomerDashboard/EditBooking/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
            return RedirectToAction("CustomerDash");
        }

        // POST: Account/CustomerDashboard/DeleteBooking/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return RedirectToAction("CustomerDash");
        }

        // GET: Account/CustomerDashboard/Venues
        public IActionResult Venues()
        {
            var venues = _context.Venues.ToList();
            return View(venues);
        }

        private int GetLoggedInUserId()
        {
            // Replace with your authentication system
            return 1; // demo
        }
    }
}