using EventEaseT.Data;                   // DbContext for EF Core
using EventEaseT.Models;                 // Event and Venue models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering; // For dropdown lists in views
using System.Linq;

public class EventController : Controller
{
    private readonly EventEaseContext _context;

    // Constructor injects the database context
    public EventController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Event/Index
    // Retrieves all events from the database.
    // Uses eager loading to include related Venue details.
    // Passes the list of events to the Index view.
    public IActionResult Index()
    {
        var events = _context.Events
            .Include(e => e.Venue) // ensures Venue is populated
            .ToList();

        return View(events);
    }

    // GET: Event/Create
    // Displays a form for creating a new event.
    // Populates a dropdown list with available venues.
    public IActionResult Create()
    {
        ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName");
        return View();
    }

    // POST: Event/Create
    // Handles form submission for creating a new event.
    // Binds only specific fields to avoid overposting.
    // Removes validation for navigation property (Venue).
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("EventName,EventDate,Description,VenueId")] Event ev)
    {
        // Ignore validation on navigation property
        ModelState.Remove("Venue");

        if (!ModelState.IsValid)
        {
            // Log validation errors to console
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Validation error: " + error.ErrorMessage);
            }

            // Repopulate dropdown if validation fails
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View(ev);
        }

        // Default status for new events
        ev.Status = "Pending";
        _context.Events.Add(ev);
        var rows = _context.SaveChanges();
        Console.WriteLine($"Rows saved: {rows}");

        return RedirectToAction("Index");
    }

    // POST: Event/Approve
    // Marks an event as Approved.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Approve(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev == null) return NotFound();

        ev.Status = "Approved";
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    // POST: Event/Deny
    // Marks an event as Denied.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Deny(int id)
    {
        var ev = _context.Events.Find(id);
        if (ev == null) return NotFound();

        ev.Status = "Denied";
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}