using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StalkerShop.Data.Models;
namespace StalkerShop.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContent content)
        {


            if (!content.Categories.Any())
            {
                content.Categories.AddRange(Categories.Select(c => c.Value));
            }
            if (!content.RadioItems.Any())
            {
                content.AddRange(
                    new RadioItem
                    {
                        name = "ДП-5В",
                        shortDesc = "Я покажу тебе дорогу!",
                        longDesc = "Хоть я и советский прибор, послужу тебе отлично!",
                        img = "/img/dp5v.jpg",
                        price = 2000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Радиометры"]
                    },
                    new RadioItem
                    {
                        name = "ИД-01",
                        shortDesc = "Мощный и стильный",
                        longDesc = "Чисто пацанский Дозиметр, бери если не gay",
                        img = "/img/id1.jpg",
                        price = 1000,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Дозиметры"]
                    },
                    new RadioItem
                    {
                        name = "ДП-22-В",
                        shortDesc = "Мне нужен заряд!",
                        longDesc = "Намного слабее чем ИД-01",
                        img = "/img/dp22v.jpg",
                        price = 500,
                        isFavourite = false,
                        available = true,
                        Category = Categories["Дозиметры"]
                    },
                    new RadioItem
                    {
                        name = "ДП-2",
                        shortDesc = "Динозавр радиометров",
                        longDesc = "Самый первый и верный радиометр!",
                        img = "/img/dp2.jpg",
                        price = 5000,
                        isFavourite = false,
                        available = true,
                        Category = Categories["Радиометры"]
                    }
                    );
            }
            content.SaveChanges();
        }
        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[]
                    {
                        new Category {categoryname = "Дозиметры", desc = "Современный вид транспорта"},
                        new Category {categoryname = "Радиометры",desc="Машины с двигателем внутреннего згорания"}
                    };
                    category = new Dictionary<string, Category>();
                    foreach (Category cat in list)
                    {
                        category.Add(cat.categoryname, cat);
                    }
                }
                return category;
            }
        }
    }
}
