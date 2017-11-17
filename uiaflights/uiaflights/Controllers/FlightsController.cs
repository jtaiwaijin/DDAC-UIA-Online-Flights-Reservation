using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using uiaflights.Data;
using uiaflights.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using uiaflights.Models.FlightBookingViewModels;

namespace uiaflights.Controllers
{
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlightsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Flights
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Flights
        //        .Include(f => f.Dest)
        //        .Include(f => f.Origin)
        //        .Include(f => f.Plane);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Search(string origin, string dest, string date, string sortOrder)
        {
            ViewData["origin"] = new SelectList(_context.Airports, "Name", "Name", origin);
            ViewData["originvalue"] = origin;
            ViewData["dest"] = new SelectList(_context.Airports, "Name", "Name", dest);
            ViewData["destvalue"] = dest;
            ViewData["date"] = date;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["EconSortParm"] = sortOrder == "econ_asc" ? "econ_desc" : "econ_asc";
            ViewData["BusSortParm"] = sortOrder == "bus_asc" ? "bus_desc" : "bus_asc";
            ViewData["FirstSortParm"] = sortOrder == "first_asc" ? "first_desc" : "first_asc";

            if (!String.IsNullOrEmpty(origin) && !String.IsNullOrEmpty(dest) && !String.IsNullOrEmpty(date))
            {
                var flights = from f in _context.Flights
                              .Include(f => f.Dest)
                              .Include(f => f.Origin)
                              .Include(f => f.Plane)
                              .Include(f => f.Tariff)
                              select f;

                if (!String.IsNullOrEmpty(origin))
                {
                    flights = flights.Where(f => f.Origin.IATA.Contains(origin) || f.Origin.Name.Contains(origin));
                }
                if (!String.IsNullOrEmpty(dest))
                {
                    flights = flights.Where(f => f.Dest.IATA.Contains(dest) || f.Dest.Name.Contains(dest));
                }
                if (!String.IsNullOrEmpty(date))
                {
                    DateTime departDate = DateTime.Parse(date);
                    if(departDate.Date <= DateTime.Now.Date)
                    {
                        ViewData["ErrorMessage"] = "Invalid Date! Flights are available for bookings from " + DateTime.Now.AddDays(1).Date.ToString() + " onwards.";
                        return View();
                    }
                    flights = flights.Where(f => f.DepartureDateTime >= departDate && f.DepartureDateTime <= departDate.AddDays(14));
                }
                switch (sortOrder)
                {
                    case "econ_desc":
                        flights = flights.OrderByDescending(f => f.Tariff.Single(t => t.CabinCl == "Economy").AdultFare);
                        break;
                    case "econ_asc":
                        flights = flights.OrderBy(f => f.Tariff.Single(t => t.CabinCl == "Economy").AdultFare);
                        break;
                    case "bus_desc":
                        flights = flights.OrderByDescending(f => f.Tariff.Single(t => t.CabinCl == "Business").AdultFare);
                        break;
                    case "bus_asc":
                        flights = flights.OrderBy(f => f.Tariff.Single(t => t.CabinCl == "Business").AdultFare);
                        break;
                    case "first_desc":
                        flights = flights.OrderByDescending(f => f.Tariff.Single(t => t.CabinCl == "First").AdultFare);
                        break;
                    case "first_asc":
                        flights = flights.OrderBy(f => f.Tariff.Single(t => t.CabinCl == "First").AdultFare);
                        break;
                    case "date_desc":
                        flights = flights.OrderByDescending(f => f.DepartureDateTime);
                        break;
                    default:
                        flights = flights.OrderBy(f => f.DepartureDateTime);
                        break;
                }
                return View(await flights.AsNoTracking().ToListAsync());
            }
            else
            {
                ViewData["ErrorMessage"] = "Please enter Origin, Destination, and Date (earliest is the next day) to search for flights!";
                return View();
            }

        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Dest)
                .Include(f => f.Origin)
                .Include(f => f.Plane)
                .AsNoTracking()
                .Include(f => f.Tickets).ThenInclude(t => t.Passenger).AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        //// GET: Flights/Create
        //public IActionResult Create()
        //{
        //    ViewData["DestAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID");
        //    ViewData["OriginAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID");
        //    ViewData["PlaneID"] = new SelectList(_context.Planes, "ID", "ID");
        //    return View();
        //}

        //// POST: Flights/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,FlightNo,OriginAirportID,DestAirportID,DepartureDateTime,ArrivalDateTime,PlaneID")] Flight flight)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(flight);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["DestAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID", flight.DestAirportID);
        //    ViewData["OriginAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID", flight.OriginAirportID);
        //    ViewData["PlaneID"] = new SelectList(_context.Planes, "ID", "ID", flight.PlaneID);
        //    return View(flight);
        //}

        //// GET: Flights/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var flight = await _context.Flights.SingleOrDefaultAsync(m => m.ID == id);
        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DestAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID", flight.DestAirportID);
        //    ViewData["OriginAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID", flight.OriginAirportID);
        //    ViewData["PlaneID"] = new SelectList(_context.Planes, "ID", "ID", flight.PlaneID);
        //    return View(flight);
        //}

        //// POST: Flights/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,FlightNo,OriginAirportID,DestAirportID,DepartureDateTime,ArrivalDateTime,PlaneID")] Flight flight)
        //{
        //    if (id != flight.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(flight);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FlightExists(flight.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["DestAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID", flight.DestAirportID);
        //    ViewData["OriginAirportID"] = new SelectList(_context.Airports, "AirportID", "AirportID", flight.OriginAirportID);
        //    ViewData["PlaneID"] = new SelectList(_context.Planes, "ID", "ID", flight.PlaneID);
        //    return View(flight);
        //}

        //// GET: Flights/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var flight = await _context.Flights
        //        .Include(f => f.Dest)
        //        .Include(f => f.Origin)
        //        .Include(f => f.Plane)
        //        .SingleOrDefaultAsync(m => m.ID == id);
        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(flight);
        //}

        //// POST: Flights/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var flight = await _context.Flights.SingleOrDefaultAsync(m => m.ID == id);
        //    _context.Flights.Remove(flight);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        // GET: Flights/Book/5
        [Authorize]
        public async Task<IActionResult> Book(int id, string date)
        {
            var flight = await _context.Flights
                .Include(f => f.Dest)
                .Include(f => f.Origin)
                .Include(f => f.Plane)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (flight == null)
            {
                return NotFound();
            }
            FlightBookingViewModel fbvm = new FlightBookingViewModel
            {
                flight = flight
            };
            IEnumerable<string> classType = new string[] { "Economy", "Business", "First" };
            ViewData["ClassType"] = new SelectList(classType);
            ViewData["origin"] = flight.Origin.Name;
            ViewData["dest"] = flight.Dest.Name;
            ViewData["date"] = date;
            return View(fbvm);
        }

        // POST: Flights/Book/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(FlightBookingViewModel model, int fid, int? bid)
        {
            Flight flight;
            var user = await _userManager.GetUserAsync(User);
            flight = _context.Flights
                .Include(f => f.Tariff)
                .Include(f => f.Origin)
                .Include(f => f.Dest)
                .Include(f => f.Plane)
                .SingleOrDefault(m => m.ID == fid);
            if (ModelState.IsValid)
            {
                Booking booking;
                if (bid == null)
                {
                    booking = new Booking { Customer = (Customer)user, Status = "Booked", Total = 0 };
                }
                else
                {
                    booking = _context.Bookings
                        .Include(b => b.Tickets)
                        .SingleOrDefault(b => b.ID == bid);
                }
                var passenger = new Passenger { Name = model.Name, Gender = model.Gender, DOB = model.DOB, Phone = model.Phone, Email = model.Email, Nationality = model.Nationality, PassportNo = model.PassportNo };
                var ticket = new Ticket { FlightID = fid, Passenger = passenger, Booking = booking, CabinCl = model.CabinCl, Price = flight.Tariff.Single(t => t.CabinCl == model.CabinCl).AdultFare };
                _context.Add(ticket);
                booking.Tickets.Add(ticket);
                booking.BookingDate = DateTime.Now;
                booking.Total += ticket.Price;
                if (bid == null)
                {
                    _context.Add(booking);
                }
                else
                {
                    _context.Update(booking);
                }
                passenger.Tickets.Add(ticket);
                _context.Add(passenger);
                await _context.SaveChangesAsync();
                ModelState.Clear();
                FlightBookingViewModel fbvm = new FlightBookingViewModel
                {
                    flight = flight,
                    booking = booking
                };
                IEnumerable<string> classType = new string[] { "Economy", "Business", "First" };
                ViewData["ClassType"] = new SelectList(classType);
                ViewData["Message"] = "Booking for " + passenger.Name + " is successful! Use form below to add new passenger.";
                return View(fbvm);
            }
            model.flight = flight;
            return View(model);
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.ID == id);
        }
    }
}
