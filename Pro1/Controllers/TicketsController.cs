using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pro1.Data;
using Pro1.Models;

namespace Pro1.Controllers
{
    public class TicketsController : Controller
    {
        private readonly Pro1Context _context;

        public TicketsController(Pro1Context context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticket.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound(); // If the ticket doesn't exist
            }

            var parts = await _context.Parts
                .Where(p => p.TicketId == id)
                .ToListAsync();
            

            ViewBag.Parts = parts; // Ensure this is a list of Parts

            return View(ticket); // Pass the ticket and parts to the view
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,Registration,Description,EmployeeId,EstimateDescription,EstimatePrice")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Registration,Description,EmployeeId,EstimateDescription,EstimatePrice,IsAccepted,PricePaid,State")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }

        // Action to show all tickets for the logged-in user
     public async Task<IActionResult> MyTickets()
    {
        var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

        if (string.IsNullOrEmpty(loggedInUser))
        {
            return RedirectToAction("Index", "Login"); // Redirect to login if not logged in
        }

        // Get the EmployeeId associated with the logged-in user
        var userLogin = await _context.Login
            .FirstOrDefaultAsync(l => l.Username == loggedInUser);

        if (userLogin == null)
        {
            return RedirectToAction("Index", "Login"); // Redirect if no valid user is found
        }

        // Retrieve all tickets for the current user
        var userTickets = await _context.Ticket
            .Where(t => t.EmployeeId == userLogin.EmployeeId) // Get only the tickets for this user
            .ToListAsync();

        return View(userTickets); // Pass the list of tickets to the view
    }

        public async Task<IActionResult> GetTicket()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(loggedInUser))
            {
                return RedirectToAction("Index", "Login"); // Redirect to login if not logged in
            }

            var unassignedTickets = await _context.Ticket
                .Where(t => t.EmployeeId == null) // Get tickets without an EmployeeId
                .ToListAsync();

            return View(unassignedTickets); // Pass unassigned tickets to the view
        }

        // Action to assign a ticket to the logged-in user
        public async Task<IActionResult> ClaimTicket(int ticketId)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return RedirectToAction("Index", "Login"); // Redirect to login if not logged in
            }

            var userLogin = await _context.Login
                .FirstOrDefaultAsync(l => l.Username == loggedInUser);

            if (userLogin == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var ticket = await _context.Ticket.FindAsync(ticketId);

            if (ticket != null && ticket.EmployeeId == null)
            {
                // Assign the ticket to the current user's EmployeeId
                ticket.EmployeeId = userLogin.EmployeeId;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("GetTicket"); // Redirect back to the view
        }

        // GET: Tickets/AddHour/5
        public async Task<IActionResult> AddHour(int? id)
        {
            if (id == null)
            {
                return NotFound(); // If no ticket ID is provided, return 404
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound(); // If the ticket doesn't exist, return 404
            }

            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var userLogin = await _context.Login.FirstOrDefaultAsync(l => l.Username == loggedInUser);

            if (userLogin == null || userLogin.EmployeeId != ticket.EmployeeId)
            {
                return Forbid(); // User isn't the owner of the ticket
            }

            // Return a view for adding the date and hour
            return View(new Callendar { TicketId = ticket.Id, EmployeeId = userLogin.EmployeeId });
        }

        // POST: Tickets/AddHour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHour([Bind("Date,Hour,TicketId,EmployeeId")] Callendar callendar)
        {
            if (ModelState.IsValid)
            {
                // Check if there's a conflict with the given date, hour, and employee
                var conflictExists = await _context.Callendar
                    .AnyAsync(c =>
                        c.Date == callendar.Date &&
                        c.Hour == callendar.Hour &&
                        c.EmployeeId == callendar.EmployeeId
                    );

                if (conflictExists)
                {
                    ModelState.AddModelError("", "There is already a booking at this time."); // Add error if conflict
                    return View(callendar); // Return to the form with the error message
                }

                _context.Callendar.Add(callendar); // Add to the callendar
                await _context.SaveChangesAsync(); // Save changes
                return RedirectToAction("MyTickets"); // Redirect back to MyTickets
            }

            return View(callendar); // If model state isn't valid, re-render with errors
        }
    }
}
