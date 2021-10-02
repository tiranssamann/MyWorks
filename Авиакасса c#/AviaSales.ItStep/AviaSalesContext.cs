using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace AviaSales.ItStep
{
    public class AviaSalesContext : DbContext
    {
        public AviaSalesContext() : base("AviaSalesConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserOnTicket> OnTickets { get; set; }
    }
   
}
