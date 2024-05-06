using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pro1.Data;
using Pro1.Models;

namespace Pro1.Controllers
{
    public class PartsController : Controller
    {
        private readonly Pro1Context _context;

        public PartsController(Pro1Context context)
        {
            _context = context;
        }

        // GET: Parts/Create
        public IActionResult Create(int ticketId)
        {
            var part = new Parts { TicketId = ticketId };
            return View(part); // Pass the part model with the associated TicketId
        }

        // POST: Parts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TicketId,unitPrice,amount")] Parts part)
        {

            if (ModelState.IsValid)
            {
                // Calculate the overall cost of the part
                var overallCost = part.unitPrice * part.amount;

                // Find the ticket associated with the part
                var ticket = await _context.Ticket.FindAsync(part.TicketId);

                if (ticket == null)
                {
                    return NotFound(); // If the ticket doesn't exist, return 404
                }

                // Add the part's overall cost to the ticket's estimate price
                ticket.EstimatePrice = (ticket.EstimatePrice) + overallCost;

                // Add the new part and update the ticket
                _context.Parts.Add(part); // Add the part to the database
                _context.Update(ticket);
                await _context.SaveChangesAsync(); // Save changes
                return RedirectToAction("Details", "Tickets", new { id = part.TicketId }); // Redirect to the ticket details
            }

            return View(part); // Re-render the view with validation errors if any
        }

    }
}
