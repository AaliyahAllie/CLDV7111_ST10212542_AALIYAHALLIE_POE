using EventEaseT.Data;
using EventEaseT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class VenueController : Controller
{
    private readonly EventEaseContext _context;

    public VenueController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Venue/Index
    public IActionResult Index()
    {
        var venues = _context.Venues.ToList();
        ViewBag.VenueCount = venues.Count;
        return View(venues);
    }

    // GET: Venue/Create
    public IActionResult Create() => View();

    // POST: Venue/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Venue venue)
    {
        if (ModelState.IsValid)
        {
            _context.Venues.Add(venue);
            var result = _context.SaveChanges();
            Console.WriteLine($"Rows affected: {result}");
            return RedirectToAction("Index");
        }

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation error: {error.ErrorMessage}");
        }

        return View(venue);
    }

    // GET: Venue/Edit/5
    public IActionResult Edit(int id)
    {
        var venue = _context.Venues.Find(id);
        if (venue == null) return NotFound();
        return View(venue);
    }

    // POST: Venue/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Venue venue)
    {
        if (ModelState.IsValid)
        {
            var existingVenue = _context.Venues.Find(venue.VenueId);
            if (existingVenue == null) return NotFound();

            existingVenue.VenueName = venue.VenueName;
            existingVenue.Location = venue.Location;
            existingVenue.Capacity = venue.Capacity;
            existingVenue.ImageUrl = venue.ImageUrl;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation error: {error.ErrorMessage}");
        }

        return View(venue);
    }
}