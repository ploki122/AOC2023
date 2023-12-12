using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Framework
{
    internal class Helper
    {
        public static long PGCD(long num1, long num2)
        {
            while (num2 != 0)
            {
                var temp = num2;
                num2 = num1 % num2;
                num1 = temp;
            }

            return num1;
        }

        public static long PPCM(long num1, long num2)
        {
            return num1 / PGCD(num1, num2) * num2;
        }
    }
}
