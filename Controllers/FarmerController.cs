using Microsoft.AspNetCore.Mvc;
using EAPD7111_Agri_Energy_Connect.Data;
using EAPD7111_Agri_Energy_Connect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace EAPD7111_Agri_Energy_Connect.Controllers
{
    [Authorize]
    public class FarmerController : Controller
    {
        private readonly AppDbContext _context;

        public FarmerController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboardfarm()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dashboardfarm(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);

            if (farmer == null)
                return Unauthorized();

            model.FarmerId = farmer.FarmerId;
            model.DateAdded = DateTime.Now;

            _context.Products.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProducts");
        }

        public async Task<IActionResult> MyProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("MyProducts", "Farmer"); 
            }

            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.UserId == userId);

            if (farmer == null)
                return Unauthorized();

            var products = farmer.Products?.ToList() ?? new List<ProductModel>();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);

            if (farmer == null || product.FarmerId != farmer.FarmerId)
                return Unauthorized();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingProduct = await _context.Products.FindAsync(model.Id);
            if (existingProduct == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);

            if (farmer == null || existingProduct.FarmerId != farmer.FarmerId)
                return Unauthorized();

            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.ProductType = model.ProductType;
            existingProduct.Price = model.Price;
            existingProduct.DateAdded = model.DateAdded;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProducts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId == userId);

            if (farmer == null || product.FarmerId != farmer.FarmerId)
                return Unauthorized();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProducts");
        }
    }
}
