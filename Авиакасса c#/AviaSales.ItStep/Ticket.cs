using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviaSales.ItStep
{
    public class Ticket
    {
        public int Id { get; set; }
        public string FirstCountry { get; set; }
        public string LastCountry { get; set; }
        public double Cost { get; set; }
        public string Date { get; set; }
    }
}
