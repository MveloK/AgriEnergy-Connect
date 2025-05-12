using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EAPD7111_Agri_Energy_Connect.Data;
using EAPD7111_Agri_Energy_Connect.Models;

namespace EAPD7111_Agri_Energy_Connect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var farmers = await _context.Farmers.ToListAsync();
            return View(farmers);
        }

        // GET: Create Farmer
        [HttpGet]
        public IActionResult CreateFarmer()
        {
            return View();
        }

        // POST: Create Farmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFarmer(FarmerModel farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(farmer);
        }

        // GET: Edit Farmer
        [HttpGet]
        public async Task<IActionResult> EditFarmer(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }
            return View(farmer);
        }

        // POST: Update Farmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFarmer(FarmerModel farmer)
        {
            if (!ModelState.IsValid)
            {
                return View(farmer);
            }

            try
            {
                _context.Update(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Farmers.Any(f => f.FarmerId == farmer.FarmerId))
                {
                    return NotFound();
                }
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFarmer(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }

            _context.Farmers.Remove(farmer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<IActionResult> FarmerProducts(int id, DateTime? fromDate, DateTime? toDate, string productType)
        {
            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.FarmerId == id);

            if (farmer == null)
                return NotFound();

            var productsQuery = farmer.Products.AsQueryable();

            if (fromDate.HasValue)
                productsQuery = productsQuery.Where(p => p.DateAdded >= fromDate.Value);

            if (toDate.HasValue)
                productsQuery = productsQuery.Where(p => p.DateAdded <= toDate.Value);

            if (!string.IsNullOrWhiteSpace(productType))
                productsQuery = productsQuery.Where(p => p.ProductType == productType);

            var productTypes = await _context.Products
                .Where(p => p.FarmerId == id)
                .Select(p => p.ProductType)
                .Distinct()
                .ToListAsync();

            var viewModel = new ProductFilterModel
            {
                FarmerId = id,
                FromDate = fromDate,
                ToDate = toDate,
                ProductType = productType,
                Products = productsQuery.ToList(),
                AvailableProductTypes = productTypes
            };

            return View("FarmerProducts", viewModel);
        }
    }
}
