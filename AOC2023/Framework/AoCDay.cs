using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOC2023.Framework
{
    internal abstract class AoCDay
    {
        private int year = 2023;
        private int day;
        private string input;
        private string test;

        public AoCDay(int dayNumber, string inputFilePath = "", string testFilePath = "")
        {
            this.day = dayNumber;
            
            if(inputFilePath != "") { 
                this.input = inputFilePath;
            }
            else
            {
                this.input = $"Inputs/Day{dayNumber}.txt";
            }

            if (testFilePath != "")
            {
                this.test = testFilePath;
            }
            else
            {
                this.test = $"Inputs/Day{dayNumber}Test.txt";
            }
        }

        public void Run()
        {
            Console.WriteLine(Environment.NewLine + $"Day {day}");
            if(!File.Exists(this.input))
            {
                if (!RetrieveInput())
                {
                    Console.WriteLine("IMPOSSIBLE DE RÉCUPÉRER LE FICHIER!");
                    return;
                }
            }
            string[] file = File.ReadAllLines(this.input);

            Console.WriteLine("Part one : " + PartOne(ref file));
            Console.WriteLine("Part two : " + PartTwo(ref file));
        }

        public void Test()
        {
            Console.WriteLine(Environment.NewLine + $"Test Day {day}");
            string[] file = File.ReadAllLines(test);

            Console.WriteLine("Part one : " + PartOne(ref file));
            Console.WriteLine("Part two : " + PartTwo(ref file));
        }

        protected abstract string PartOne(ref string[] file);
        protected abstract string PartTwo(ref string[] file);

        private bool RetrieveInput()
        {

            using HttpClientHandler handler = new() { UseCookies = false };
            using HttpClient client = new(handler);

            // provide the authentication token
            string sSESSION = ReadSESSION();

            if (sSESSION == "")
            {
                Console.WriteLine("SESSION unavailable");
                return false;
            }

            // Add the session cookie
            client.DefaultRequestHeaders.Add("Cookie", $"session={sSESSION}");
            using HttpResponseMessage response = client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input").Result;

            if (response.IsSuccessStatusCode)
            {
                string inputText = response.Content.ReadAsStringAsync().Result;

                if (!Directory.Exists("Inputs"))
                {
                    Directory.CreateDirectory("Inputs");
                }
                File.WriteAllText(this.input, inputText);
                return true;
            }
            else 
            {
                return false;
            }
        }

        private string ReadSESSION()
        {
            return File.ReadAllText("SESSION.config");
        }
    }

}
