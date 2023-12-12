using System.Collections.Generic;
using AOC2023.Days;
using AOC2023.Framework;

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

            //new Day1().Run();
            //new Day2().Run();
            //new Day3().Run();

            //new Day4().Run();
            //new Day5().Run();
            //new Day6().Run();
            //new Day7().Run();
            //new Day8().Run();
            //new Day9().Run();
            //new Day10().Run();

            //new Day11().Run();
            new Day12().Run();
            //new Day13().Run();
            //new Day14().Run();
            //new Day15().Run();
            //new Day16().Run();
            //new Day17().Run();

            //new Day18().Run();
            //new Day19().Run();
            //new Day20().Run();
            //new Day21().Run();
            //new Day22().Run();
            //new Day23().Run();
            //new Day24().Run();
        }
    }
}