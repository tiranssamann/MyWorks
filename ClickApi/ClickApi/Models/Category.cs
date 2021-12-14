
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClickApi.Models
{
    public class Category
    {
        [JsonIgnore]
        public int Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
