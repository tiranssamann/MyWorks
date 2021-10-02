using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StalkerShop.Data.Models
{
    public class Category
    {
        public int id { get; set; }
        public string categoryname { get; set; }
        public string desc { get; set; }
        public List<RadioItem> radioItems { get; set; }
    }
}
