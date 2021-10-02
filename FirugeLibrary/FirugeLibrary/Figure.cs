using System;
using System.Collections.Generic;
using System.Text;

namespace FirugeLibrary
{
    public class Figure 
    {

        public static double Area(params double[] args)
        {
            double S = 0;
            double p;
            // for circle
            if(args.Length == 1)
            {
                if(args[0] <= 0)
                {
                    throw new Exception("Radius should be > 0");
                }
                S = Math.PI * Math.Pow(args[0], 2); 
            }
            // for square
            else if(args.Length == 2)
            {
                if (args[0] <= 0 || args[1] <= 0)
                {
                    throw new Exception("Sides should be > 0");
                }
                S = args[0] * args[1];
            }
            // for triangle
            else if (args.Length == 3)
            {
                if (args[0] <= 0 || args[1] <= 0 || args[3] <= 0)
                {
                    throw new Exception("Sides should be > 0");
                }
                p = (args[0] + args[1] + args[2]) / 2;

                S = Math.Sqrt(p * (p - args[0]) * (p - args[1]) * (p - args[2]));
            }
            else throw new Exception("The minimum number of parameters is 1, the maximum is 3");
            return S;
        }


    }
}
