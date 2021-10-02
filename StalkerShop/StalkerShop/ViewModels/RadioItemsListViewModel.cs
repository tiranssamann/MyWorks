using StalkerShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StalkerShop.ViewModels
{
    public class RadioItemsListViewModel
    {
        public IEnumerable<RadioItem> GetCars { get; set; }
        public string currCategory { get; set; }
    }
}
