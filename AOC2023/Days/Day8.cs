using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 8)]
    internal class Day8 : AoCDay
    {
        protected override string PartOne(ref string[] file)
        {
            int nSteps = 0;

            string path = file[0];
            Dictionary<string, (string, string)> maps = new();
            
            foreach (string line in file[2..])
            {
                maps[line[0..3]] = (line[7..10], line[12..15]);
            }

            for (string nextStep = "AAA"; nextStep != "ZZZ";)
            {
                int pos = nSteps % path.Length;
                if (path[pos] == 'L')
                    nextStep = maps[nextStep].Item1;
                else
                    nextStep = maps[nextStep].Item2;

                nSteps++;
            }

            return nSteps.ToString(); //18157
        }

        protected override string PartTwo(ref string[] file)
        {


            string path = file[0];
            Dictionary<string, (string, string)> maps = new();
            Dictionary<string, string> keys = new();
            Dictionary<string, int> steps = new();
            foreach (string line in file[2..])
            {
                maps[line[0..3]] = (line[7..10], line[12..15]);
                if (line[2] == 'A')
                    keys.Add(line[0..3], line[0..3]);
            }

            int nSteps = 0;
            while (keys.Count > 0) { 
                int pos = nSteps % path.Length;
                if (path[pos] == 'L')
                    foreach (var key in keys)
                        keys[key.Key] = maps[key.Value].Item1;
                else
                    foreach (var key in keys)
                        keys[key.Key] = maps[key.Value].Item2;

                nSteps++;

                foreach (var key in keys)
                    if (key.Value.EndsWith("Z"))
                    {
                        steps.Add(key.Key, nSteps);
                        keys.Remove(key.Key);
                    }
            }

            long result = 1;
            foreach (var step in steps.Values)
            {
                result = Helper.PPCM(result, step);
            }

            return result.ToString();   //14299763833181
        }
    }
}
