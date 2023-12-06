using System.Collections.Generic;
using AOC2023.Days;

namespace AOC2023
{
    internal class Program
    {
        static void Main()
        {
            if (!File.Exists("SESSION.config"))
            {
                File.CreateText("SESSION.config");
                Console.WriteLine("SESSION file initialized, please fill with cookie.");
            }

            new Day1().Run();
            new Day2().Run();
            new Day3().Run();
            new Day4().Run();
            new Day5().Run();
        }
    }
}