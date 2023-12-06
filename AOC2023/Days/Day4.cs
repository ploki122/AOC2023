using AOC2023.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Days
{
    [AoC(2023, 4)]
    internal class Day4 : AoCDay
    {

        protected override string PartOne(ref string[] file)
        {
            int nSum = 0;

            foreach (string line in file)
            {
                string[] winningNumbers = line.Split(": ")[1].Split(" | ")[0].Split(" ");
                string[] cardNumbers = line.Split(": ")[1].Split("|")[1].Split(" ");
                string[] empty = { "" };

                int nMatches = cardNumbers.Intersect(winningNumbers).Except(empty).Count();

                if (nMatches > 0)
                    nSum += (int) Math.Pow(2, nMatches - 1);
            }

            return nSum.ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            int nSum = 0;

            int[] weights = Enumerable.Repeat(1, file.Length).ToArray();
            for (int i = 0; i < file.Length; i++)
            {
                string line = file[i];

                string[] winningNumbers = line.Split(": ")[1].Split(" | ")[0].Split(" ");
                string[] cardNumbers = line.Split(": ")[1].Split("|")[1].Split(" ");
                string[] empty = { "" };

                int nMatches = cardNumbers.Intersect(winningNumbers).Except(empty).Count();
                nSum += weights[i];
                for (int cardNo = i+1; cardNo <= i + nMatches; cardNo++)
                    weights[cardNo] += weights[i];
            }

            return nSum.ToString();
        }
    }
}
