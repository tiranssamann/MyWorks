using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StalkerShop.Data.Models;
namespace StalkerShop.Data.Interfaces
{
    public interface IRadioCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
