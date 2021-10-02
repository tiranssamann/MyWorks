using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StalkerShop.Data.Interfaces;
using StalkerShop.Data.Models;
namespace StalkerShop.Data.Repository
{
    public class OrdersRepository : IAllOrders
    {
        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCar;
        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCar)
        {
            this.appDBContent = appDBContent;
            this.shopCar = shopCar;
        }
        public void createOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            appDBContent.Orders.Add(order);
            appDBContent.SaveChanges();
            var items = shopCar.listShopItems;
            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    Radioid = el.car.id,
                    orderId = order.id,
                    price = el.car.price
                };
                appDBContent.OrderDetails.Add(orderDetail);
            }
            appDBContent.SaveChanges();
        }
    }
}
