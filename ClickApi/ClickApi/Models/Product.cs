using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickApi.Models
{
    public class Product
    {

        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }

    }
}
