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
    public class ShopRadioItemController : Controller
    {
        private readonly IAllRadioItems _carRep;
        private readonly ShopCart _shopCar;
        public ShopRadioItemController(IAllRadioItems carRepository, ShopCart shopCar)
        {
            _carRep = carRepository;
            _shopCar = shopCar;
        }
        public ViewResult Index()
        {
            var items = _shopCar.getShopItems();
            _shopCar.listShopItems = items;

            var obj = new ShopRadioItemsViewModel
            {
                ShopCar = _shopCar
            };
            return View(obj);
        }
        public RedirectToActionResult addToCart(int id)
        {
            var item = _carRep.AllRadioItem.FirstOrDefault(i => i.id == id);
            if (item != null)
            {
                _shopCar.AddToCart(item);
            }
            return RedirectToAction("Index");
        }
        
    }
}
