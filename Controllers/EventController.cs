using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseT.Data;
using EventEaseT.Models;

namespace EventEaseT.Controllers
{
    public class EventController : Controller
    {
        private readonly EventEaseContext _context;

        public EventController(EventEaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.Include(e => e.Venue).ToListAsync();
            return View(events);
        }

        // GET: Event/Create
        public async Task<IActionResult> Create()
        {
            var vm = new EventCreateViewModel
            {
                Venues = await _context.Venues.ToListAsync(),
                Events = await _context.Events.Include(e => e.Venue).ToListAsync()
            };
            return View(vm);
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var ev = vm.NewEvent;
                ev.Status = "Pending";
                _context.Events.Add(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // repopulate lists on failure
            vm.Venues = await _context.Venues.ToListAsync();
            vm.Events = await _context.Events.Include(e => e.Venue).ToListAsync();
            return View(vm);
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
}