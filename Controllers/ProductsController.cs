using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Controllers
{
    public class ProductsController : Controller
    {
        //Database
        private readonly DBConnect _context;
        public ProductsController(DBConnect context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index(int? FarmerId)
        {
            //Check if a farmer has been selected and save it to the ViwwData
            if (FarmerId.HasValue)
            {
                ViewData["FarmerId"] = FarmerId;
            }
            // Declare variables
            Farmer farmer = new Farmer();
            List<Product> products = new List<Product>();
            CombinedModel combinedModel = new CombinedModel();
            try
            {
                // Pull data from the database that matches the farmer's ID.
                if (FarmerId.HasValue)
                {
                    farmer = await _context.Farmers.FindAsync(FarmerId) ?? new Farmer();
                    products = await _context.Products.Where(p => p.FarmerId == FarmerId).ToListAsync();
                    combinedModel = new CombinedModel(farmer, products);
                    return View(combinedModel);
                }
                // Pull all product data from the database.
                else
                {
                    farmer = new Farmer();
                    products = await _context.Products.ToListAsync();
                    combinedModel = new CombinedModel(farmer, products);
                    return View(combinedModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            combinedModel = new CombinedModel(farmer, products);
            return View(combinedModel);
        }

        // Details
        public async Task<IActionResult> Details(int? id, int? FarmerId)
        {
            Product? product = new Product();
            // Check if an ID has been passed to the method
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                // Pull data from the database
                product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
                // Check if the data has been found
                if (product == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            return View(product);
        }

        // Get Create
        [HttpGet]
        public IActionResult Create(int? FarmerId)
        {
            // Check if a farmer has been selected and pass it to the ViewBag
            if (FarmerId.HasValue)
            {
                ViewBag.FarmerId = FarmerId;
            }
            return View();
        }

        // Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? FarmerId, [Bind("Id,Name,Category,Farm,ProductionDate,FarmerId")] Product product)
        {
            //Get farmer and check if it is valid to be saved to the variable.
            ViewBag.FarmerId = FarmerId;
            if (FarmerId.HasValue)
            {
                product.FarmerId = (int)FarmerId;
            }
            // Check if the model is valid.
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { farmerId = FarmerId });
            }
            return View(product);
        }

        // Get Edit
        public async Task<IActionResult> Edit(int? id)
        {
            Product? product = new Product();
            // Check if an ID has been passed
            if (id == null)
            {
                return NotFound();
            }
            // Try to pull data from the database
            try
            {
                product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            return View(product);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? FarmerId, [Bind("Id,Name,Category,Farm,ProductionDate,FarmerId")] Product product)
        {
            // Check if the ID is valid.
            if (id != product.Id)
            {
                return NotFound();
            }
            // Check if the model is valid.
            if (ModelState.IsValid)
            {
                // Try to update the database
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { farmerId = FarmerId });
            }
            return View(product);
        }

        // Get Delete
        public async Task<IActionResult> Delete(int? id)
        {
            Product? product = new Product();
            if (id == null)
            {
                return NotFound();
            }
            // Try to pull data from the database
            try
            {
                product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            return View(product);
        }

        // Post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? FarmerId)
        {
            Product? product = new Product();
            // Try to delete data from the database.
            try
            {
                // First check if the product is there.
                product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                }
                // Delete the product.
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                NotFound();
            }
            return RedirectToAction("Index", new
            {
                farmerId = FarmerId
            });
        }
        // Check if a product exists.
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
