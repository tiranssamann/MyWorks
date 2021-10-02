using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StalkerShop.Data.Models;
namespace StalkerShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<RadioItem> FavCars { get; set; }
    }
}
