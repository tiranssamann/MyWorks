using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public abstract class Person : Interface1
    {
        public string Surname { get; set; }
        public string name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string group { get; set; }
        public abstract string Print();
        public override bool Equals(object obj)
        {
            return this.Login == (obj as Person).Login &&
                this.Password == (obj as Person).Password;
        }
        public static bool operator ==(Person obj1, Person obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Person obj1, Person obj2)
        {
            return !obj1.Equals(obj2);
        }
    }
    public enum Roles{ Administrator = 0, Student = 1 }
}
