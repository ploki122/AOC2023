using AOC2023.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2023.Days
{
    [AoC(2023, 3)]
    internal partial class Day3 : AoCDay
    {
        protected override string PartOne(ref string[] file)
        {
            int nSum = 0;
            for (int nCoordY = 0; nCoordY < file.Length; nCoordY++)
            {
                string line = file[nCoordY];
                for (int nCoordX=0; nCoordX<line.Length; nCoordX++)
                {
                    int nStart=-1, nEnd=-1;
                    if (char.IsAsciiDigit(line.ElementAt(nCoordX))) {
                        nStart = nCoordX;
                        while(nCoordX < line.Length && char.IsAsciiDigit(line.ElementAt(nCoordX)))
                        {
                            nEnd = nCoordX;
                            nCoordX++;
                        }
                    }

                    if (nStart > -1)
                    {
                        int nNo = int.Parse(file[nCoordY][nStart..(nEnd+1)]);

                        int nPreviousLine = Math.Max(nCoordY - 1, 0);
                        int nNextLine = Math.Min(nCoordY + 1, file.Length-1);
                        int nRefStart = Math.Max(nStart - 1, 0);
                        int nRefEnd = Math.Min(nEnd + 1, line.Length-1)+1;

                        if ((SpecialChar().Replace(file[nCoordY][nRefStart..nRefEnd], "").Length > 0) ||
                                (SpecialChar().Replace(file[nPreviousLine][nRefStart..nRefEnd], "").Length > 0) ||
                                (SpecialChar().Replace(file[nNextLine][nRefStart..nRefEnd], "").Length > 0))
                        {
                            nSum += nNo;
                        }
                    }
                }
            }
            return nSum.ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            int nSum = 0;
            for (int nCoordY = 0; nCoordY < file.Length; nCoordY++)
            {
                string line = file[nCoordY];
                for (int nCoordX = 0; nCoordX < line.Length; nCoordX++)
                {
                    if (line.ElementAt(nCoordX) == '*')
                    {
                        List<int> results = new();
                        if (nCoordY > 0)
                        {
                            if (!char.IsAsciiDigit(file[nCoordY - 1].ElementAt(nCoordX)))
                            {
                                if (nCoordX > 0 && ParseNumber(file[nCoordY - 1], nCoordX - 1) > -1)
                                    results.Add(ParseNumber(file[nCoordY - 1], nCoordX - 1));

                                if (nCoordX < line.Length - 1 && ParseNumber(file[nCoordY - 1], nCoordX + 1) > -1)
                                    results.Add(ParseNumber(file[nCoordY - 1], nCoordX + 1));
                            }
                            else if (ParseNumber(file[nCoordY - 1], nCoordX) > -1)
                                results.Add(ParseNumber(file[nCoordY - 1], nCoordX));
                        }

                        if (nCoordX > 0 && ParseNumber(file[nCoordY], nCoordX - 1) > -1)
                            results.Add(ParseNumber(file[nCoordY], nCoordX - 1));

                        if (nCoordX < line.Length - 1 && ParseNumber(file[nCoordY], nCoordX + 1) > -1)
                            results.Add(ParseNumber(file[nCoordY], nCoordX + 1));

                        if (nCoordY < file.Length)
                        {
                            if (!char.IsAsciiDigit(file[nCoordY + 1].ElementAt(nCoordX)))
                            {
                                if (nCoordX > 0 && ParseNumber(file[nCoordY + 1], nCoordX - 1) > -1)
                                    results.Add(ParseNumber(file[nCoordY + 1], nCoordX - 1));

                                if (nCoordX < line.Length - 1 && ParseNumber(file[nCoordY + 1], nCoordX + 1) > -1)
                                    results.Add(ParseNumber(file[nCoordY + 1], nCoordX + 1));
                            }
                            else if (ParseNumber(file[nCoordY + 1], nCoordX) > -1)
                                results.Add(ParseNumber(file[nCoordY + 1], nCoordX));
                        }

                        if (results.Count == 2)
                            nSum += results[0] * results[1];
                    }
                }
            }
            return nSum.ToString();
        }

        [GeneratedRegex(@"[0-9\.]")]
        private static partial Regex SpecialChar();

        private static int ParseNumber(string sLine, int nCoordX)
        {
            if (!char.IsAsciiDigit(sLine.ElementAt(nCoordX)))
                return -1;

            int nStart = nCoordX, nEnd = nCoordX + 1;

            while(nStart>0 && char.IsAsciiDigit(sLine.ElementAt(nStart-1)))
                nStart--;

            while (nEnd < sLine.Length && char.IsAsciiDigit(sLine.ElementAt(nEnd)))
                nEnd++;

            return int.Parse(sLine[nStart..nEnd]);
        }
    }
}
