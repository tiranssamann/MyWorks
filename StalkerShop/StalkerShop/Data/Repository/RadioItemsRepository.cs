using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StalkerShop.Data.Interfaces;
using StalkerShop.Data.Models;
namespace StalkerShop.Data.Repository
{
    public class RadioItemsRepository : IAllRadioItems
    {
        public readonly AppDBContent appDBContent;
        public RadioItemsRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<RadioItem> AllRadioItem => appDBContent.RadioItems.Include(c => c.Category);


        public IEnumerable<RadioItem> getFavRadioItem => appDBContent.RadioItems.Where(p => p.isFavourite).Include(c => c.Category);

        public RadioItem GetobjectRadioItem(int carid) => appDBContent.RadioItems.FirstOrDefault(p => p.id == carid);
    }
}
