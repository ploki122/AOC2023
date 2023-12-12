using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 9)]
    internal class Day9 : AoCDay
    {
        protected override string PartOne(ref string[] file)
        {
            long nSum = 0;

            foreach (var line in file)
            {
                List<int[]> series = new() { line.Split(" ").Select(x => int.Parse(x)).ToArray() };

                while (!IsZeroed(series.Last()))
                {
                    int[] oldSeries = series.Last();
                    int[] newSeries = new int[oldSeries.Length - 1];
                    for(int i=1; i < oldSeries.Length; i++)
                    {
                        newSeries[i-1] = oldSeries[i] - oldSeries[i-1];
                    }
                    series.Add(newSeries);
                }

                int next = 0;
                for (int i = series.Count-2; i>= 0; i--)
                {
                    next += series[i][^1];
                }

                nSum += next;
            }

            return nSum.ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            long nSum = 0;

            foreach (var line in file)
            {
                List<int[]> series = new() { line.Split(" ").Select(x => int.Parse(x)).ToArray() };

                while (!IsZeroed(series.Last()))
                {
                    int[] oldSeries = series.Last();
                    int[] newSeries = new int[oldSeries.Length - 1];
                    for (int i = 1; i < oldSeries.Length; i++)
                    {
                        newSeries[i - 1] = oldSeries[i] - oldSeries[i - 1];
                    }
                    series.Add(newSeries);
                }

                int next = 0;
                for (int i = series.Count - 2; i >= 0; i--)
                {
                    next = series[i][0] - next;
                }

                nSum += next;
            }

            return nSum.ToString(); //
        }

        private static bool IsZeroed(int[] series)
        {
            foreach (var n in series)
                if (n != 0) 
                    return false;

            return true;
        }
    }
}
