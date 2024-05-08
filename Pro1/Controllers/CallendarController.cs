using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pro1.Data;
using Pro1.Models;


namespace Pro1.Controllers
{
    public class CallendarController : Controller
    {
        private readonly Pro1Context _context;

        public CallendarController(Pro1Context context)
        {
            _context = context;
        }

        public IActionResult EmployeeCalendar() // This action returns the view for the employee calendar
        {
            return View(); // Return the view for the calendar
        }

        public async Task<IActionResult> GetEmployeeEvents()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return Forbid(); // If not logged in, return forbidden
            }

            var userLogin = await _context.Login.FirstOrDefaultAsync(l => l.Username == loggedInUser);

            if (userLogin == null || userLogin.EmployeeId == null)
            {
                return Forbid(); // If no valid login, return forbidden
            }

            // Retrieve calendar events for the logged-in employee
            var events = await _context.Callendar
                .Where(c => c.EmployeeId == userLogin.EmployeeId) // Ensure you're getting the correct events
                .Select(c => new
                {
                    id = c.Id,
                    title = $"Ticket {c.TicketId}", // Customize the title for FullCalendar
                    start = c.Date, // Start time for the event
                    allDay = false // Specify if it's an all-day event
                })
                .ToListAsync(); // Return the list of events

            return Json(events); // Return the events as JSON for FullCalendar
        }

        public IActionResult Calendar() // This action returns the view for the employee calendar
        {
            return View(); // Return the view for the calendar
        }

        public async Task<IActionResult> GetAllEvents()
        {
            // Retrieve all calendar events and join with Employee and Ticket to get additional information
            var events = await _context.Callendar
                .Join(_context.Employee,
                    callendar => callendar.EmployeeId,
                    employee => employee.Id,
                    (callendar, employee) => new
                    {
                        Callendar = callendar,
                        Employee = employee
                    })
                .Join(_context.Ticket,
                    joined => joined.Callendar.TicketId,
                    ticket => ticket.Id,
                    (joined, ticket) => new
                    {
                        Callendar = joined.Callendar,
                        Employee = joined.Employee,
                        Ticket = ticket
                    })
                .Select(x => new
                {
                    id = x.Callendar.Id,
                    title = $"{x.Employee.Name} - Ticket {x.Ticket.Registration}", // Event title
                    start = x.Callendar.Date, // Event start time
                    allDay = false
                })
                .ToListAsync(); // Get all calendar events

            return Json(events); // Return the events as JSON for FullCalendar
        }

    }
}
