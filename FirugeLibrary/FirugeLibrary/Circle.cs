using System;
using System.Collections.Generic;
using System.Text;

namespace FirugeLibrary
{
    public class Circle
    {
        public static double AreaCircle(double r)
        {
            double S = 0;
            if (r <= 0)
            {
                throw new Exception("Radius should be > 0");
            }
            S = Math.PI * Math.Pow(r, 2);
            return S;
        }
    }
}
