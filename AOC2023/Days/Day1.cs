using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 1)]
    internal class Day1 : AoCDay
    {
        public Day1(string inputFilePath = "", string testFilePath = "") : base(inputFilePath, testFilePath)
        {
        }

        protected override string PartOne(ref string[] file)
        {
            int sum = 0;
            Dictionary<string, int> keyList = new Dictionary<string, int>
            {
                ["1"] = 1,
                ["2"] = 2,
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5,
                ["6"] = 6,
                ["7"] = 7,
                ["8"] = 8,
                ["9"] = 9
            };

            foreach (string line in file)
            {
                int posMin = line.Length + 1, posMax = -1, valMin = 0, valMax = 0;
                foreach (string key in keyList.Keys)
                {
                    int pos = line.IndexOf(key);

                    //local min
                    if ((pos > -1) && (pos < posMin))
                    {
                        posMin = pos;
                        valMin = keyList[key];
                    }

                    pos = line.LastIndexOf(key);
                    //local max
                    if (pos > posMax)
                    {
                        posMax = pos;
                        valMax = keyList[key];
                    }
                }
                sum += int.Parse(valMin.ToString() + valMax.ToString());
            }

            return sum.ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            int sum = 0;
            Dictionary<string, int> keyList = new Dictionary<string, int>
            {
                ["1"] = 1,
                ["2"] = 2,
                ["3"] = 3,
                ["4"] = 4,
                ["5"] = 5,
                ["6"] = 6,
                ["7"] = 7,
                ["8"] = 8,
                ["9"] = 9,
                ["one"] = 1,
                ["two"] = 2,
                ["three"] = 3,
                ["four"] = 4,
                ["five"] = 5,
                ["six"] = 6,
                ["seven"] = 7,
                ["eight"] = 8,
                ["nine"] = 9
            };

            foreach (string line in file)
            {
                int posMin = line.Length + 1, posMax = -1, valMin = 0, valMax = 0;
                foreach (string key in keyList.Keys)
                {
                    int pos = line.IndexOf(key);

                    //local min
                    if ((pos > -1) && (pos < posMin))
                    {
                        posMin = pos;
                        valMin = keyList[key];
                    }

                    pos = line.LastIndexOf(key);
                    //local max
                    if (pos > posMax)
                    {
                        posMax = pos;
                        valMax = keyList[key];
                    }
                }
                sum += int.Parse(valMin.ToString() + valMax.ToString());
            }

            return sum.ToString();
        }
    }
}
