using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StalkerShop.Data.Interfaces;
using StalkerShop.Data.Models;
using StalkerShop.ViewModels;
namespace StalkerShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllRadioItems _carRep;
        public HomeController(IAllRadioItems carRepository)
        {
            _carRep = carRepository;
        }
        public ViewResult Index()
        {
            var homeCars = new HomeViewModel
            {
                FavCars = _carRep.getFavRadioItem
            };
            return View(homeCars);
        }
    }
}
