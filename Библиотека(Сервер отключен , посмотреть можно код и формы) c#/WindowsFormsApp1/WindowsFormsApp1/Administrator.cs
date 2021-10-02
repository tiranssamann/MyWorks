using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Administrator : Person
    {
        List<Person> students;
        public Administrator()
        {
            students = new List<Person>();
        }
        public List<Person> STUDENTS
        {
            get
            {
                return students;
            }
        }
        public override string Print()
        {
            return string.Format("{0} {1}", name, Lastname);
        }
    }
}
