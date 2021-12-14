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
    public class CategoriesController : ControllerBase
    {
        private readonly ProductWithCategoryContext _context;

        public CategoriesController(ProductWithCategoryContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _context.Category.Include("Products").ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost("addCategory")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }
        [HttpPut("editCategory")]
        public async Task<ActionResult<Category>> editCategory(int id,string categoryName)
        {
            var category = await _context.Category.FindAsync(id);
            Category cat = category;
            cat.CategoryName = categoryName;
            _context.Entry(cat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = cat.Id }, cat);
        }
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }
            var product = _context.Product.Select(p => p).Where(p => p.CategoryId == category.Id).FirstOrDefault();
            product.CategoryId = null;
            _context.Entry(product).State = EntityState.Modified;
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
