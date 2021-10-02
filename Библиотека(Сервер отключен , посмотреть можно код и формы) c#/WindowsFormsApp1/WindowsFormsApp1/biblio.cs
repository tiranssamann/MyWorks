using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class biblio : Person
    {
        public override string Print()
        {
            return string.Format("{0} {1}", name, Lastname);
        }
    }
}
