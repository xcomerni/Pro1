using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pro1.Models;
using Pro1.Data;

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
            _context.Parts.Add(part); // Add the new part to the database
            await _context.SaveChangesAsync(); // Save changes
            return RedirectToAction("Details", "Tickets", new { id = part.TicketId }); // Redirect to the ticket details
        }

        return View(part); // Re-render the view with validation errors if any
    }
}