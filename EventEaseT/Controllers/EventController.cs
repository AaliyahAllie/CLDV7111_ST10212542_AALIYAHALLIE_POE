using EventEaseT.Data;
using EventEaseT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

public class EventController : Controller
{
    private readonly EventEaseContext _context;

    public EventController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Event/Index
    public IActionResult Index()
    {
        var events = _context.Events
            .Include(e => e.Venue) // ensures Venue is populated
            .ToList();

        return View(events);
    }

    // GET: Event/Create
    public IActionResult Create()
    {
        ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName");
        return View();
    }

    // POST: Event/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("EventName,EventDate,Description,VenueId")] Event ev)
    {
        // Ignore validation on navigation property
        ModelState.Remove("Venue");

        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Validation error: " + error.ErrorMessage);
            }

            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "VenueName");
            return View(ev);
        }

        ev.Status = "Pending";
        _context.Events.Add(ev);
        var rows = _context.SaveChanges();
        Console.WriteLine($"Rows saved: {rows}");

        return RedirectToAction("Index");
    }

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