using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClickApi.Models;

namespace ClickApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductWithCategoryContext _context;

        public ProductsController(ProductWithCategoryContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpGet("ProductsByCategory/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int id)
        {                 
            return await _context.Product.Select(p => p).Where(p => p.CategoryId == id).ToListAsync();
        }
        [HttpPost("addProduct")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        // POST: api/Products/editProduct
        [HttpPut("editProduct")]
        public async Task<ActionResult<Product>> editProduct(int id, string productName, int? CatId)
        {
            var product = await _context.Product.FindAsync(id);
            Product prod = product;
            prod.ProductName = productName;
            if (CatId.HasValue) { 
            prod.CategoryId = CatId;
            }
            _context.Entry(prod).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = prod.Id }, prod);
        }
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
