using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

using EAPD7111_Agri_Energy_Connect.Data;
using EAPD7111_Agri_Energy_Connect.Models;

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim().ToLower();
            var farmer = await _context.Farmers
                .FirstOrDefaultAsync(f => f.UserId.ToLower().Trim() == userId);

            if (farmer == null)
            {
                Console.WriteLine($"Dashboardfarm: No farmer found for userId {userId}");
                return Unauthorized();
            }

            model.FarmerId = farmer.FarmerId;
            model.DateAdded = DateTime.Now;

            _context.Products.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProducts");
        }

        [HttpGet]
        public async Task<IActionResult> MyProducts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim().ToLower();
            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("MyProducts: UserId is null or empty");
                return Unauthorized();
            }

            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.UserId.ToLower().Trim() == userId);

            if (farmer == null)
            {
                Console.WriteLine($"MyProducts: No farmer found for userId {userId}");
                return Unauthorized();
            }

            var products = farmer.Products?.ToList() ?? new List<ProductModel>();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim().ToLower();
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId.ToLower().Trim() == userId);

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim().ToLower();
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId.ToLower().Trim() == userId);

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)?.Trim().ToLower();
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.UserId.ToLower().Trim() == userId);

            if (farmer == null || product.FarmerId != farmer.FarmerId)
                return Unauthorized();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyProducts");
        }

        // Debug endpoint to help inspect current logged-in user and claims
        [HttpGet]
        public IActionResult DebugUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var claims = User.Claims.Select(c => $"{c.Type} = {c.Value}");
            return Content($"UserId: {userId}\nClaims:\n{string.Join("\n", claims)}");
        }
    }
}
