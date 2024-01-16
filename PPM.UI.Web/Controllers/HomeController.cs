using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.DAL;
using PPM.Model;

namespace PPM.Ui.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PPMContext _context;

        public HomeController(PPMContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userRole") != null)
            {
                ViewBag.UserSession = HttpContext.Session.GetString("userRole");
               // Retrieve user details based on employeeId
                int employeeId = HttpContext.Session.GetInt32("employeeId") ?? 0;
                var userDetails = _context.Employee.Find(employeeId);

                if (userDetails != null)
                {
                    return View(userDetails);
                }

                // Handle if the user details are not found
                return View("Error");
            }
            else
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToAction("Login");
            }
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("userRole") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Employee user)
        {
            var validUser = await _context.Employee
                .Join(
                    _context.Role,
                    employee => employee.RoleId,
                    role => role.RoleId,
                    (employee, role) => new { Employee = employee, RoleName = role.Name }
                )
                .FirstOrDefaultAsync(
                    u => u.Employee.Email == user.Email && u.Employee.Password == user.Password
                );

            if (validUser != null)
            {
                HttpContext.Session.SetString("userRole", validUser.RoleName);
                HttpContext.Session.SetInt32("employeeId", validUser.Employee.EmployeeId);
                HttpContext.Session.SetString(
                    "userName",
                    $"{validUser.Employee.FirstName} {validUser.Employee.LastName}"
                );
                TempData["User"] = HttpContext.Session.GetString("userName");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Invalid Username or Password";
                return View();
            }
        }

       
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("userRole") != null)
            {
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index");
        }
    }
}
