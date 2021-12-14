using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClickApi.Models;

namespace ClickApi.Models
{
    public class ProductWithCategoryContext : DbContext
    {
        public ProductWithCategoryContext (DbContextOptions<ProductWithCategoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ClickApi.Models.Category> Category { get; set; }

        public DbSet<ClickApi.Models.Product> Product { get; set; }
    }
}
