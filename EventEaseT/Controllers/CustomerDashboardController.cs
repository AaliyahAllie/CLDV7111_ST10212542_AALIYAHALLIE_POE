using EventEaseT.Data;                   // DbContext for EF Core
using EventEaseT.Models;                 // Booking, Event, Venue models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventEaseT.Controllers.Account
{
    // CustomerDashboardController manages customer-facing functionality:
    // viewing bookings, creating new bookings, editing, deleting, and browsing venues.
    public class CustomerDashboardController : Controller
    {
        private readonly EventEaseContext _context;

        // Constructor injects the database context
        public CustomerDashboardController(EventEaseContext context)
        {
            _context = context;
        }

        // GET: /Account/CustomerDashboard/CustomerDash
        // Displays the logged-in customer's bookings.
        // Includes related Event and Venue details for richer display.
        public IActionResult CustomerDash()
        {
            var userId = GetLoggedInUserId();

            var bookings = _context.Bookings
                .Include(b => b.Event)   // Load related Event details
                .Include(b => b.Venue)   // Load related Venue details
                .Where(b => b.UserId == userId) // Filter by logged-in user
                .ToList();

            return View(bookings);
        }

        // GET: Account/CustomerDashboard/CreateBooking
        // Displays a form for creating a new booking.
        // Pre-populates dropdowns with available events and venues.
        [HttpGet]
        public IActionResult CreateBooking(int? venueId)
        {
            ViewBag.Events = _context.Events.ToList();
            ViewBag.Venues = _context.Venues.ToList();

            var booking = new Booking();
            if (venueId.HasValue)
            {
                booking.VenueId = venueId.Value; // Pre-select venue if provided
            }

            return View(booking);
        }

        // POST: Account/CustomerDashboard/CreateBooking
        // Saves a new booking to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBooking(Booking booking)
        {
            booking.UserId = GetLoggedInUserId(); // Assign logged-in user
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("CustomerDash");
        }

        // GET: Account/CustomerDashboard/EditBooking/5
        // Loads an existing booking for editing.
        public IActionResult EditBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        // POST: Account/CustomerDashboard/EditBooking/5
        // Updates an existing booking in the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
            return RedirectToAction("CustomerDash");
        }

        // POST: Account/CustomerDashboard/DeleteBooking/5
        // Deletes a booking from the database.
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
        // Displays a list of all venues available for booking.
        public IActionResult Venues()
        {
            var venues = _context.Venues.ToList();
            return View(venues);
        }

        private int GetLoggedInUserId()
        {
            return 1; 
        }
    }
}