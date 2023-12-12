using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 11)]
    internal class Day11: AoCDay
    {
        protected override string PartOne(ref string[] file)
        {
            List<int> emptyColumns = new();
            for(int x = 0; x < file[0].Length; x++)
            {
                if (Column(ref file, x).Replace(".", null).Length == 0)
                    emptyColumns.Add(x);
            }

            List<(int, int)> galaxies = new();
            List<string> workFile = new();
            for (int y = 0; y < file.Length; y++)
            {
                string newLine = "";
                for (int x = 0; x < file[y].Length; x++)
                {
                    if (file[y][x] == '#')
                        galaxies.Add((workFile.Count, newLine.Length));

                    newLine += file[y][x];
                    if(emptyColumns.Contains(x))
                        newLine += file[y][x];
                }
                workFile.Add(newLine);

                if (file[y].Replace(".", null).Length == 0)
                    workFile.Add(newLine);
            }

            long nSum = 0;

            for (int i = 0; i < galaxies.Count; i++)
                for (int j = i + 1; j < galaxies.Count; j++)
                    nSum += Math.Abs(galaxies[i].Item2 - galaxies[j].Item2) + Math.Abs(galaxies[i].Item1 - galaxies[j].Item1);

            return nSum.ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            List<int> emptyColumns = new();
            for (int x = 0; x < file[0].Length; x++)
            {
                if (Column(ref file, x).Replace(".", null).Length == 0)
                    emptyColumns.Add(x);
            }

            List<(int, int)> galaxies = new();
            int actualY = 0;
            for (int y = 0; y < file.Length; y++, actualY++)
            {
                int actualX = 0;
                for (int x = 0; x < file[y].Length; x++, actualX++)
                {
                    if (file[y][x] == '#')
                        galaxies.Add((actualY, actualX));

                    if (emptyColumns.Contains(x))
                        actualX += 999999;
                }

                if (file[y].Replace(".", null).Length == 0)
                    actualY += 999999;
            }

            long nSum = 0;

            for (int i = 0; i < galaxies.Count; i++)
                for (int j = i + 1; j < galaxies.Count; j++)
                    nSum += Math.Abs(galaxies[i].Item2 - galaxies[j].Item2) + Math.Abs(galaxies[i].Item1 - galaxies[j].Item1);

            return nSum.ToString();
        }

        protected static string Column(ref string[] file, int colNo)
        {
            return new string(file.Select(x => x[colNo]).ToArray());
        }
    }
}
