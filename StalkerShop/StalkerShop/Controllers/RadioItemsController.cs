using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StalkerShop.Data.Interfaces;
using StalkerShop.Data.Models;
using StalkerShop.ViewModels;
namespace StalkerShop.Controllers
{
    public class RadioItemsController :Controller
    {
        private readonly IAllRadioItems _allCars;
        private readonly IRadioCategory _allcategories;
        public RadioItemsController(IAllRadioItems iallcars, IRadioCategory icarscat)
        {
            _allCars = iallcars;
            _allcategories = icarscat;
        }
        [Route("RadioItems/List")]
        [Route("RadioItems/List/{category}")]
        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<RadioItem> cars = null;
            string curCat = "";
            if (string.IsNullOrEmpty(category))
            {
                cars = _allCars.AllRadioItem.OrderBy(i => i.id);

            }
            else
            {
                if (string.Equals("dvig", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = _allCars.AllRadioItem.Where(i => i.Category.categoryname.Equals("Радиометры")).OrderBy(i => i.id);
                    curCat = "Радиометры";
                }
                else if (string.Equals("electro", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = _allCars.AllRadioItem.Where(i => i.Category.categoryname.Equals("Дозиметры")).OrderBy(i => i.id);
                    curCat = "Дозиметры";
                }


            }
            ViewBag.Title = "Приблуды сталкера";
            var carobj = new RadioItemsListViewModel
            {
                GetCars = cars,
                currCategory = curCat
            };
            return View(carobj);
        }
        
        public ViewResult RadioDetails(int id)
        {
            var radiodetail = _allCars.GetobjectRadioItem(id);
            return View(radiodetail);
        }
    }
}
