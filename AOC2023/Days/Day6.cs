using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 6)]
    internal class Day6 : AoCDay
    {
        protected override string PartOne(ref string[] file)
        {
            long nSum = 1;

            long[] times = file[0].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
            long[] distances = file[1].Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
            for (int i=0; i<times.Length; i++)
            {
                var roots = quadraticRoot(-1, times[i], -distances[i]);
                nSum *= roots.Item2 - roots.Item1 + 1;
            }
            return nSum.ToString(); //1312850
        }

        protected override string PartTwo(ref string[] file)
        {

            file[0] = file[0].Replace(" ", "");
            file[1] = file[1].Replace(" ", "");

            return PartOne(ref file); //36749103
        }

        private (long, long) quadraticRoot(long a, long b, long c)
        {
            long delta = (long)Math.Pow(b, 2) - 4 * a * c;

            double root1 = (-b + Math.Sqrt(delta)) / (2 * a);
            double root2 = (-b - Math.Sqrt(delta)) / (2 * a);

            return ((long)Math.Floor(Math.Min(root1, root2) + 1),
                    (long)Math.Ceiling(Math.Max(root1, root2) - 1));
        }
    }
}
