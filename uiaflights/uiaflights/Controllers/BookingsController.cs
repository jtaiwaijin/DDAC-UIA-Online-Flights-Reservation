using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using uiaflights.Data;
using uiaflights.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using uiaflights.Models.FlightBookingViewModels;

namespace uiaflights.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string sortOrder)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            //ViewData["FlightSortParm"] = sortOrder == "flight_asc" ? "flight_desc" : "flight_asc";
            //ViewData["OriginSortParm"] = sortOrder == "origin_asc" ? "origin_desc" : "origin_asc";
            //ViewData["DestSortParm"] = sortOrder == "dest_asc" ? "dest_desc" : "dest_asc";
            //ViewData["DateSortParm"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            var bookings = from b in _context.Bookings
                           .Include(b => b.Customer)
                           .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Origin)
                           .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Dest)
                           .Include(b => b.Tickets).ThenInclude(t => t.Passenger)
                           .Where(b => b.Customer == (Customer)user)
                           select b;
            switch (sortOrder)
            {
                //case "date_desc":
                //    bookings = bookings.OrderByDescending(b => b.Tickets.FirstOrDefault().Flight.DepartureDateTime);
                //    break;
                //case "date_asc":
                //    bookings = bookings.OrderBy(b => b.Tickets.FirstOrDefault().Flight.DepartureDateTime);
                //    break;
                //case "dest_desc":
                //    bookings = bookings.OrderByDescending(b => b.Tickets.FirstOrDefault().Flight.Dest.Name);
                //    break;
                //case "dest_asc":
                //    bookings = bookings.OrderBy(b => b.Tickets.FirstOrDefault().Flight.Dest.Name);
                //    break;
                //case "origin_desc":
                //    bookings = bookings.OrderByDescending(b => b.Tickets.FirstOrDefault().Flight.Origin.Name);
                //    break;
                //case "origin_asc":
                //    bookings = bookings.OrderBy(b => b.Tickets.FirstOrDefault().Flight.Origin.Name);
                //    break;
                //case "flight_desc":
                //    bookings = bookings.OrderByDescending(b => b.Tickets.FirstOrDefault().Flight.FlightNo);
                //    break;
                //case "flight_asc":
                //    bookings = bookings.OrderBy(b => b.Tickets.FirstOrDefault().Flight.FlightNo);
                //    break;
                //case "id_desc":
                //    bookings = bookings.OrderByDescending(b => b.ID);
                //    break;
                default:
                    bookings = bookings.OrderBy(b => b.ID);
                    break;
            }
            return View(await bookings.AsNoTracking().ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Origin)
                .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Dest)
                .Include(b => b.Tickets).ThenInclude(t => t.Passenger)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Origin)
                .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Dest)
                .Include(b => b.Tickets).ThenInclude(t => t.Passenger)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }
            FlightBookingViewModel fbvm = new FlightBookingViewModel
            {
                flight = booking.Tickets.FirstOrDefault().Flight,
                booking = booking
            };
            return View(fbvm);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Origin)
                .Include(b => b.Tickets).ThenInclude(t => t.Flight).ThenInclude(f => f.Dest)
                .Include(b => b.Tickets).ThenInclude(t => t.Passenger)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Tickets)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var tickets = booking.Tickets;
                foreach (Ticket t in tickets)
                {
                    _context.Tickets.Remove(t);
                }
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }
    }
}
