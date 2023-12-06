using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Framework
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class AoCAttribute : Attribute
    {
        public AoCAttribute(int year, int day)
        {
            Year = year;
            Day = day;
        }

        public int Year { get; }
        public int Day { get; }
    }
}
