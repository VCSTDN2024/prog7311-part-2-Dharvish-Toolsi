using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Controllers
{
    public class CredentialsController : Controller
    {
        // Database.
        private readonly DBConnect _context;
        public CredentialsController(DBConnect context)
        {
            _context = context;
        }
        // Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // Post Login for employee
        [HttpPost]
        public IActionResult EmployeeLogin(Credential model)
        {
            if (ModelState.IsValid)
            {
                // Temporary hardcoded username and password. Not needed for final version
                if (model.Username == "admin" && model.Password == "password")
                {
                    return RedirectToAction("Index", "Farmers");
                }
                else
                {
                    // Display error message
                    ModelState.AddModelError("", "Invalid username or password");
                    TempData["EmployeeLoginError"] = "Invalid username or password";
                    return View("Login");
                }
            }
            return View(model);
        }
        // Post Login for farmer
        [HttpPost]
        public IActionResult FarmerLogin(Credential model)
        {
            if (ModelState.IsValid)
            {
                // Check if farmer credintials are valid.
                Farmer? user = _context.Farmers.FirstOrDefault(c => c.Id.ToString() == model.Username && c.Password == model.Password);
                if (user != null)
                {
                    // Redirect to products page
                    return RedirectToAction("Index", "Products", new { farmerId = user.Id });
                }
                else
                {
                    // Display error message
                    ModelState.AddModelError("", "Invalid username or password");
                    TempData["FarmerLoginError"] = "Invalid username or password";
                    return View("Login");
                }
            }
            return View(model);
        }
    }
}