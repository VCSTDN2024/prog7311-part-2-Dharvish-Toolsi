using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Controllers
{
    public class FarmersController : Controller
    {
        // Database
        private readonly DBConnect _context;
        public FarmersController(DBConnect context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            List<Farmer> farmers = new List<Farmer>();
            // Try pull data from the database.
            try
            {
                farmers = await _context.Farmers.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(farmers);
        }
        // Details
        public async Task<IActionResult> Details(int? id)
        {
            Farmer? farmer = new Farmer();
            // Check if an ID has been passed to the method
            if (id == null)
            {
                return NotFound();
            }
            // Try to pull data from the database.
            try
            {
                farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.Id == id);
                if (farmer == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            return View(farmer);
        }

        // Get Create
        public IActionResult Create()
        {
            return View();
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Farm,Password")] Farmer farmer)
        {
            // Try push data to the database.
            try
            {
                // Check if the model is valid.
                if (ModelState.IsValid)
                {
                    _context.Add(farmer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(farmer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View(farmer);
            }
        }
        // Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }
            return View(farmer);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Farm,Password")] Farmer farmer)
        {
            // Check if an ID has been passed to the method.
            if (id != farmer.Id)
            {
                return NotFound();
            }
            // Check if the model is valid.
            if (ModelState.IsValid)
            {
                // Try to update the database.
                try
                {
                    _context.Update(farmer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmer.Id))
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
            return View(farmer);
        }

        // Delete
        public async Task<IActionResult> Delete(int? id)
        {
            Farmer? farmer = new Farmer();
            // Check if an ID has been passed to the method.
            if (id == null)
            {
                return NotFound();
            }
            // Try to pull data from the database.
            try
            {
                farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.Id == id);
                if (farmer == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            return View(farmer);
        }

        // Post Delete.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Farmer? farmer = new Farmer();
            // Try to delete data from the database.
            try
            {
                farmer = await _context.Farmers.FindAsync(id);
                // Check if the farmer is there.
                if (farmer != null)
                {
                    // Delete the farmer.
                    _context.Farmers.Remove(farmer);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return RedirectToAction(nameof(Index));
        }
        // Check if a farmer exists.
        private bool FarmerExists(int id)
        {
            return _context.Farmers.Any(e => e.Id == id);
        }
        // Get Display products
        [HttpGet]
        public async Task<IActionResult> DisplayProducts(int? FarmerId, DateTime? startDate, DateTime? endDate, string categoryType)
        {
            // Declare variables
            List<Product> products = new List<Product>();
            List<string> categories = new List<string>();
            // Try to pull data from the database.
            try
            {
                products = await _context.Products.Where(p => p.FarmerId == FarmerId).ToListAsync();
                categories = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();
                // Add categories to ViewBag
                ViewBag.Categories = categories;
                // Check if any date filters have been applied.
                if (startDate.HasValue && endDate.HasValue)
                {
                    products = products.Where(p => p.ProductionDate >= startDate && p.ProductionDate <= endDate).ToList();
                }
                // Check if a category filter has been applied.
                if (!string.IsNullOrEmpty(categoryType))
                {
                    products = products.Where(p => p.Category == categoryType).ToList();
                }
                // Return a normal list instead of null or zero.
                if (products == null || products.Count == 0)
                {
                    products = await _context.Products.Where(p => p.FarmerId == FarmerId).ToListAsync();
                    ViewBag.ErrorMessage = "No products found.";
                }
                return View(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(new List<Product>());
        }
    }
}
