using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class book
    {
        public int id { get; set; }
        public string NameOfBook { get; set; }
        public string Author { get; set; }
        public string Valueofbook { get; set; }
        public int Valuestr { get; set; }
        public int Rate { get; set; }
        public void printbook()
        {
            Console.WriteLine(NameOfBook + " " + Author + " " + Valueofbook + " " + Valuestr + " " + Rate);
        }
    }
}
