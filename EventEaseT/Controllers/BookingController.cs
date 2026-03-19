using EventEaseT.Data;                   // DbContext for EF Core
using EventEaseT.Models;                 // Booking, Event, and Venue models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookingController : Controller
{
    private readonly EventEaseContext _context;

    // Constructor injects the database context
    public BookingController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Booking/Index
    // This action retrieves all bookings from the database.
    // It includes related Event and Venue data using eager loading (Include).
    // The result is passed to the Index view for display.
    public IActionResult Index()
    {
        var bookings = _context.Bookings
            .Include(b => b.Event)   // Load related Event details
            .Include(b => b.Venue)   // Load related Venue details
            .ToList();

        return View(bookings);       // Return the list of bookings to the view
    }
}