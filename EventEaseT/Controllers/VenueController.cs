using EventEaseT.Data;                   // DbContext for EF Core
using EventEaseT.Models;                 // Venue model
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class VenueController : Controller
{
    private readonly EventEaseContext _context;

    // Constructor injects the database context
    public VenueController(EventEaseContext context)
    {
        _context = context;
    }

    // GET: Venue/Index
    // Retrieves all venues from the database.
    // Counts the total number of venues and stores it in ViewBag for display.
    // Passes the list of venues to the Index view.
    public IActionResult Index()
    {
        var venues = _context.Venues.ToList();
        ViewBag.VenueCount = venues.Count;
        return View(venues);
    }

    // GET: Venue/Create
    // Displays a form for creating a new venue.
    public IActionResult Create() => View();

    // POST: Venue/Create
    // Handles form submission for creating a new venue.
    // Validates the model before saving to the database.
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

        // Log validation errors if model state is invalid
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation error: {error.ErrorMessage}");
        }

        return View(venue);
    }

    // GET: Venue/Edit/5
    // Loads an existing venue for editing.
    // Returns NotFound if the venue does not exist.
    public IActionResult Edit(int id)
    {
        var venue = _context.Venues.Find(id);
        if (venue == null) return NotFound();
        return View(venue);
    }

    // POST: Venue/Edit/5
    // Updates an existing venue in the database.
    // Uses a safer pattern: fetch existing venue, update its fields, then save.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Venue venue)
    {
        if (ModelState.IsValid)
        {
            var existingVenue = _context.Venues.Find(venue.VenueId);
            if (existingVenue == null) return NotFound();

            // Update fields
            existingVenue.VenueName = venue.VenueName;
            existingVenue.Location = venue.Location;
            existingVenue.Capacity = venue.Capacity;
            existingVenue.ImageUrl = venue.ImageUrl;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Log validation errors if model state is invalid
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation error: {error.ErrorMessage}");
        }

        return View(venue);
    }
}