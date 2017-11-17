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

namespace uiaflights.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Passenger)
                .Include(t => t.Booking)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Passenger)
                .Include(t => t.Booking)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }
            IEnumerable<string> classType = new string[] { "Economy", "Business", "First" };
            ViewData["ClassType"] = new SelectList(classType, ticket.CabinCl);
            return View(ticket);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticketToUpdate = await _context.Tickets
                .Include(t => t.Passenger)
                .Include(t => t.Booking)
                .Include(t => t.Flight).ThenInclude(f => f.Tariff)
                .SingleOrDefaultAsync(t => t.ID == id);
            var bookingUrl = "/Bookings/Edit/" + ticketToUpdate.Booking.ID;
            if (await TryUpdateModelAsync<Ticket>(
                ticketToUpdate,
                "",
                t => t.Passenger, t => t.CabinCl))
            {
                try
                {
                    var oldPrice = ticketToUpdate.Price;
                    ticketToUpdate.Price = ticketToUpdate.Flight.Tariff.Single(t => t.CabinCl == ticketToUpdate.CabinCl).AdultFare;
                    ticketToUpdate.Booking.Total = ticketToUpdate.Booking.Total - oldPrice + ticketToUpdate.Price;
                    await _context.SaveChangesAsync();
                    return Redirect(bookingUrl);
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            IEnumerable<string> classType = new string[] { "Economy", "Business", "First" };
            ViewData["ClassType"] = new SelectList(classType, ticketToUpdate.CabinCl);
            return View(ticketToUpdate);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Passenger)
                .Include(t => t.Booking)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Booking)
                .SingleOrDefaultAsync(m => m.ID == id);
            var bookingUrl = "/Bookings/Edit/" + ticket.Booking.ID;
            if (ticket == null)
            {
                return Redirect(bookingUrl);
            }
            try
            {
                ticket.Booking.Total = ticket.Booking.Total - ticket.Price;
                _context.Update(ticket.Booking);
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
                return Redirect(bookingUrl);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.ID == id);
        }
    }
}
