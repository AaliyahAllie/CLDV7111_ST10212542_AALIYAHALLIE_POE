using EventEaseT.Data;
using EventEaseT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookingController : Controller
{
    private readonly EventEaseContext _context;

    public BookingController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Booking/Index
    public IActionResult Index()
    {
        var bookings = _context.Bookings
            .Include(b => b.Event)
            .Include(b => b.Venue)
            .ToList();

        return View(bookings);
    }
}