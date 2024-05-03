using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pro1.Data;
using Pro1.Models;

namespace Pro1.Controllers
{
    public class LoginController : Controller
    {
        private readonly Pro1Context _context;

        public LoginController(Pro1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new Login()); // Ensure the view has a model instance
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Login
                    .FirstOrDefaultAsync(u =>
                        u.Username == loginModel.Username &&
                        u.Password == loginModel.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("LoggedInUser", user.Username);

                    if (user.IsAdmin)
                    {
                        return RedirectToAction("Index", "Employees"); // Redirect if admin
                    }

                    return RedirectToAction("Welcome"); // Redirect for regular users
                }

                // If no user is found, add a model error
                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If model state isn't valid, ensure error messages are displayed
            return View("Index", loginModel);
        }

        public IActionResult Welcome()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            if (loggedInUser == null)
            {
                return RedirectToAction("Index"); // Redirect back to login if no session
            }

            ViewBag.User = loggedInUser; // Pass the logged-in user to the view
            return View(); // Render the Welcome page
        }
    }
}