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

        public async Task<IActionResult> MyCallendar()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                return RedirectToAction("Index", "Login"); // Redirect to login if no user is logged in
            }

            // Get the EmployeeId for the logged-in user
            var userLogin = await _context.Login.FirstOrDefaultAsync(l => l.Username == loggedInUser);

            if (userLogin == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Retrieve all Callendar records for this EmployeeId
            var myCallendar = await _context.Callendar
                .Where(c => c.EmployeeId == userLogin.EmployeeId)
                .ToListAsync();

            return View(myCallendar); // Pass the Callendar records to the view
        }
    }
}
