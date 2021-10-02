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
    public class OrderController : Controller
    {
        private readonly IAllOrders orders;
        private readonly ShopCart shopCar;
        public OrderController(IAllOrders orders, ShopCart shopCar)
        {
            this.orders = orders;
            this.shopCar = shopCar;
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            shopCar.listShopItems = shopCar.getShopItems();
            if (shopCar.listShopItems.Count == 0)
            {
                ModelState.AddModelError("", "У вас должны быть товары");
            }
            if (ModelState.IsValid)
            {
                orders.createOrder(order);
                return RedirectToAction("Complete");
            }
            return View(order);
        }
        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}
