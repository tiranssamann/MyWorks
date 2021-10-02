using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace StalkerShop.Data.Models
{
    public class Order
    {
        [BindNever]
        public int id { get; set; }
        [Display(Name = "Введите имя")]
        [Required(ErrorMessage = "Длина имени не менее 1 символа")]
        public string name { get; set; }
        [Display(Name = "Введите Фамилию")]
        [Required(ErrorMessage = "Длина Фамилит не менее 1 символа")]
        public string surname { get; set; }
        [Display(Name = "Введите адрес")]
        public string adress { get; set; }
        [Display(Name = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
        [Display(Name = "Введите email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Длина email не менее 5 символа")]
        public string email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
