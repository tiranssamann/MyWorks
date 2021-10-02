using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StalkerShop.Data.Interfaces;
using StalkerShop.Data.Models;
namespace StalkerShop.Data.Repository
{
    public class CategoryRepository : IRadioCategory
    {
        public readonly AppDBContent appDBContent;
        public CategoryRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Category> AllCategories => appDBContent.Categories;
    }
}
