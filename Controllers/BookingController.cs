using Microsoft.AspNetCore.Mvc;
using RailwayTicketSystem.Models;

namespace RailwayTicketSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly RailwayDbContext _context;

        public BookingController(RailwayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                booking.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
                _context.Bookings.Add(booking);
                _context.SaveChanges();

                TempData["Success"] = "Ticket Booked Successfully!";
                return RedirectToAction("Success");
            }

            return View(booking);
        }

        public IActionResult Success()
        {
            return View();
        }

        // Get: //Booking/History
        public IActionResult History()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var bookings = _context.Bookings
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.DateOfJourney)
            .ToList();

            return View(bookings);
        }
    }
}
