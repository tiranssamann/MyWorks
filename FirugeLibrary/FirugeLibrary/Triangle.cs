using System;
using System.Collections.Generic;
using System.Text;

namespace FirugeLibrary
{
    public class Triangle 
    {
        public static double AreaTriangle(params double[] args)
        {
            double S = 0;
            double p;
            if (args.Length == 3)
            {
                if (args[0] <= 0 || args[1] <= 0 || args[3] <= 0)
                {
                    throw new Exception("Sides should be > 0");
                }
                p = (args[0] + args[1] + args[2]) / 2;

                S = Math.Sqrt(p * (p - args[0]) * (p - args[1]) * (p - args[2]));
            }
            else if (args.Length == 2)
            {
                if (args[0] <= 0 || args[1] <= 0)
                {
                    throw new Exception("Sides should be > 0");
                }
                S = 0.5 * args[0] * args[1];
            }
            else throw new Exception("The minimum number of parameters is 2, the maximum is 3");
            return S;
        }
        public static bool IsRightTriangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                throw new Exception("Sides should be > 0");
            }
            double maxSide = Math.Max(Math.Max(a, b), c);
            return (maxSide * maxSide) == (maxSide == a ? b * b + c * c :
                                            maxSide == b ? a * a + c * c :
                                            a * a + b * b);
        }
    }
}
