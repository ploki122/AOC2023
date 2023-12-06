using AOC2023.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Days
{
    [AoC(2023, 5)]
    internal class Day5 : AoCDay
    {
        //input = destination, source, size
        //output = start, end, offset
        private static (long, long, long) ParseAsTuple(string str)
        {
            string[] frags = str.Split(' ');
            return (
                long.Parse(frags[1]),
                long.Parse(frags[1]) + long.Parse(frags[2]) - 1,
                long.Parse(frags[0]) - long.Parse(frags[1])
            );
        }

        protected override string PartOne(ref string[] file)
        {
            List<long> seeds = new();

            foreach(string seed in file[0][7..].Split(' '))
                seeds.Add(long.Parse(seed));

            string lastHeader = "seeds";

            Dictionary<string, List<(long, long, long)>> maps = new();
            for (var it = 2;it<file.Length; it++)
            {
                if (file[it] == "")
                    continue;

                if (file[it].EndsWith(" map:"))
                {
                    lastHeader = file[it].Split(' ')[0];
                    maps[lastHeader] = new();
                    continue;
                }

                maps[lastHeader].Add(ParseAsTuple(file[it]));
            }

            long nMin = long.MaxValue;

            foreach(var seed in seeds)
            {
                long nResult = seed;
                foreach(var map in maps)
                {
                    foreach (var entry in map.Value)
                    {
                        if (nResult >= entry.Item1 && nResult < entry.Item2)
                        {
                            nResult += entry.Item3;
                            break;
                        }
                    }
                }

                if (nResult < nMin)
                    nMin = nResult;
            }

            return nMin.ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            List<(long, long)> seedRanges = new();
                     
            string[] seeds = file[0][7..].Split(' ');
            for (int i = 0; i < seeds.Length; i +=2)
                seedRanges.Add((long.Parse(seeds[i]), long.Parse(seeds[i]) + long.Parse(seeds[i + 1]) - 1));

            string lastHeader = "seeds";

            Dictionary<string, List<(long, long, long)>> maps = new();
            for (var it = 2; it < file.Length; it++)
            {
                if (file[it] == "")
                    continue;

                if (file[it].EndsWith(" map:"))
                {
                    lastHeader = file[it].Split(' ')[0];
                    maps[lastHeader] = new();
                    continue;
                }

                maps[lastHeader].Add(ParseAsTuple(file[it]));
            }

            long nMin = long.MaxValue;

            foreach (var range in seedRanges)
            {
                List<(long, long)> results = new() { range };
                foreach (var map in maps)
                {
                    var futureResults = results.ToList();
                    foreach (var result in results)
                    {
                        List<(long, long)> missingRanges = new() { result };
                        foreach (var entry in map.Value)
                        {
                            //There is an overlap
                            if (result.Item2 >= entry.Item1 && result.Item1 <= entry.Item2)
                            {
                                //Create the overlap range
                                futureResults.Remove(result);
                                futureResults.Add((long.Max(result.Item1, entry.Item1) + entry.Item3, long.Min(result.Item2, entry.Item2) + entry.Item3));

                                var futureMissing = missingRanges.ToList();
                                foreach (var missing in missingRanges)
                                {
                                    //There is an overlap
                                    if (entry.Item2 >= missing.Item1 && entry.Item1 <= missing.Item2)
                                    {
                                        futureMissing.Remove(missing);

                                        //Splits the missing range
                                        if (entry.Item1 > missing.Item1 && entry.Item2 < missing.Item2)
                                        {
                                            futureMissing.Add((missing.Item1, entry.Item1 - 1));
                                            futureMissing.Add((entry.Item2 + 1, missing.Item2));
                                        }
                                        //Shortens the range from the left
                                        else if (entry.Item1 <= missing.Item1 && entry.Item2 >= missing.Item1 && missing.Item2 > entry.Item2 + 1)
                                        {
                                            futureMissing.Add((entry.Item2 + 1, missing.Item2));
                                        }
                                        //Shortens the range from the right
                                        else if (entry.Item1 <= missing.Item2 && entry.Item2 >= missing.Item2 && entry.Item1 - 1 > missing.Item1)
                                        {
                                            futureMissing.Add((missing.Item1, entry.Item1 - 1));
                                        }
                                    }
                                }
                                missingRanges = futureMissing;
                            }
                        }
                        futureResults.AddRange(missingRanges);
                    }
                    results = futureResults;
                }
                

                foreach (var result in results)
                    if (result.Item1 < nMin)
                        nMin = result.Item1;
            }

            return nMin.ToString();
        }
    }
}
