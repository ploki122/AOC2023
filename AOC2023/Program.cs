﻿using System.Collections.Generic;
using AOC2023.Days;

namespace AOC2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("SESSION.config"))
            {
                StreamWriter sw = File.CreateText("SESSION.config");
                Console.WriteLine("SESSION file initialized, please fill with cookie.");
            }

            new Day1().Run();
        }
    }
}