using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StalkerShop.Data.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int Radioid { get; set; }
        public uint price { get; set; }
        public virtual RadioItem Radio { get; set; }
        public virtual Order order { get; set; }
    }
}
