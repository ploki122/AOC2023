using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 2)]
    internal class Day2 : AoCDay
    {
        const int MAX_BLUE = 14;
        const int MAX_GREEN = 13;
        const int MAX_RED = 12;
        protected override string PartOne(ref string[] file)
        {
            int nSum = 0;
            int nGameNo = 1;
            foreach (string line in file)
            {
                int nMaxBlue = 0, nMaxGreen = 0, nMaxRed = 0;
                bool bCheckBlue = false, bCheckGreen = false, bCheckRed = false;
                foreach (string fragment in line.Split(" ").Reverse().ToList())
                {
                    if (bCheckBlue)
                        nMaxBlue = Math.Max(nMaxBlue, byte.Parse(fragment.Replace(",", "").Replace(";", "")));
                    else if (bCheckGreen)
                        nMaxGreen = Math.Max(nMaxGreen, byte.Parse(fragment.Replace(",", "").Replace(";", "")));
                    else if (bCheckRed)
                        nMaxRed = Math.Max(nMaxRed, byte.Parse(fragment.Replace(",", "").Replace(";", "")));

                    if (fragment.StartsWith("blue"))
                        bCheckBlue = true;
                    else if (fragment.StartsWith("red"))
                        bCheckRed = true;
                    else if (fragment.StartsWith("green"))
                        bCheckGreen = true;
                    else
                    {
                        bCheckGreen = false;
                        bCheckRed = false;
                        bCheckBlue = false;
                    }
                }

                if (nMaxBlue <= MAX_BLUE && nMaxGreen <= MAX_GREEN && nMaxRed <= MAX_RED)
                {
                    nSum += nGameNo;
                }

                nGameNo++;
            }
            return nSum.ToString(); //2164
        }

        protected override string PartTwo(ref string[] file)
        {
            int nSum = 0;
            foreach (string line in file)
            {
                int nMaxBlue = 0, nMaxGreen = 0, nMaxRed = 0;
                bool bCheckBlue = false, bCheckGreen = false, bCheckRed = false;
                foreach (string fragment in line.Split(" ").Reverse().ToList())
                {
                    if (bCheckBlue)
                        nMaxBlue = Math.Max(nMaxBlue, byte.Parse(fragment.Replace(",", "").Replace(";", "")));
                    else if (bCheckGreen)
                        nMaxGreen = Math.Max(nMaxGreen, byte.Parse(fragment.Replace(",", "").Replace(";", "")));
                    else if (bCheckRed)
                        nMaxRed = Math.Max(nMaxRed, byte.Parse(fragment.Replace(",", "").Replace(";", "")));

                    if (fragment.StartsWith("blue"))
                        bCheckBlue = true;
                    else if (fragment.StartsWith("red"))
                        bCheckRed = true;
                    else if (fragment.StartsWith("green"))
                        bCheckGreen = true;
                    else
                    {
                        bCheckGreen = false;
                        bCheckRed = false;
                        bCheckBlue = false;
                    }
                }
                nSum += nMaxRed * nMaxBlue * nMaxGreen; 
            }
            return nSum.ToString(); //69929
        }
    }
}
