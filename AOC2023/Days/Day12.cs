using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 12)]
    internal class Day12 : AoCDay
    {
        private static readonly short DEBUG_LEVEL = 99;
        private static readonly List<string> EXPECTED_RESULTS = new() { "22", " 2", " 8", " 8", "11", " 1", " 2", " 5", "23", " 4", " 3", " 3", " 1", " 2", "28", "20", " 5", "20", "52", " 6", " 6", "40", " 7", " 6", " 8", " 4", " 6", " 1", " 6", " 3", " 1", " 2", " 3", " 1", " 4", " 2", " 3", " 3", " 6", " 4", " 3", " 1", " 2", " 6", " 4", " 4", "11", " 8", " 2", " 2", " 9", " 8", "13", " 5", "16", " 5", " 5", " 4", " 2", " 6", " 2", " 2", " 18", " 3", " 3", " 9", " 6", "18", " 2", " 1", " 6", " 4", "10", " 4", " 5", "10", " 9", " 7", " 6", " 2", " 5", "19", " 2", "45"};
        protected override string PartOne(ref string[] file)
        {
            long nSum = 0;
            int n = 0;
            foreach (var line in file)
            {
                string day = line.Split(" ")[0].Trim('.'); //Trim functional days at the beginning and end, to make things simpler.
                List<int> blocks = line.Split(" ")[1].Split(",").Select(x => int.Parse(x)).ToList();

                var res = Nonosolve(day, blocks);
                nSum += res;

                if (DEBUG_LEVEL >= 0) Console.WriteLine($"{(EXPECTED_RESULTS.Count > n ? EXPECTED_RESULTS[n] : "??")} {res}=>{line}");
                n++;
            }

            return nSum.ToString(); //7794 too high
                                    //7280 too high
                                    //5053
                                    //6995
                                    //7216
        }

        protected override string PartTwo(ref string[] file)
        {

            return "0"; //36749103
        }

        private static long Nonosolve(string line, List<int> blocks, int nLevel = 1)
        {
            if (DEBUG_LEVEL >= nLevel) Console.WriteLine($"{new string(' ', 2 * nLevel)}Nonosolve({line}, {string.Join(',', blocks)})");

            line = line.Trim('.');

            while (line.Contains(".."))
                line = line.Replace("..", ".");

            //Strip from the start
            while (blocks.Count > 0 && line.Length > 0)
            {
                //Replace the start with the first block when it starts with a #.
                if (line.Length > 0 && line[0] == '#')
                {
                    if (line[..blocks[0]].Contains('.') || (line.Length > blocks[0] && line[blocks[0]] == '#')) //we can never replace a . with a #
                        return 0;
                    else if (blocks[0] == line.Length)
                        line = "";
                    else
                        line = line[(blocks[0] + 1)..].TrimStart('.');

                    blocks.RemoveAt(0);
                    continue;
                }

                //Remove the start if the first ??? sequence cannot fit because of working days.
                if (line.Contains('.') && line.IndexOf('.') < blocks[0])
                {
                    if (line[..line.IndexOf('.')].Contains('#')) // we can never replace a # with a .
                        return 0;

                    line = line[line.IndexOf('.')..].TrimStart('.');
                    continue;
                }

                //Remove the first character if the first block would be followed by a broken day (#)
                if (line.Length > blocks[0] && line[blocks[0]] == '#')
                {
                    line = line[1..].TrimStart('.');
                    continue;
                }

                //Remove the start, and the first block, if it fits exactly and is forced to be there.
                if (line.Length > blocks[0] && line[..blocks[0]].Contains('#') && line[blocks[0]] == '.')
                {
                    line = line[blocks[0]..].TrimStart('.');
                    blocks.RemoveAt(0);
                    continue;
                }

                break;
            }

            //Strip from the end
            while (blocks.Count > 0 && line.Length > 0)
            {

                //Replace the end with the last block when it ends with a #.
                if (line.Length > 0 && line[^1] == '#')
                {
                    if (line[^blocks[^1]..].Contains('.') || (line.Length > blocks[^1] && line[^(blocks[^1] + 1)] == '#')) //We can't remove a #, or include a .
                        return 0;
                    else if (blocks[^1] == line.Length)
                        line = "";
                    else
                        line = line[..^(blocks[^1] + 1)].TrimEnd('.');

                    blocks.RemoveAt(blocks.Count - 1);
                    continue;
                }

                //Remove the end if the last ??? sequence cannot fit because of working days.
                if (line.Contains('.') && line.LastIndexOf('.') >= line.Length - blocks[^1])
                {
                    
                    if (line[line.LastIndexOf('.')..].Contains('#')) // we can never replace a # with a .
                        return 0;

                    line = line[..line.LastIndexOf('.')].TrimEnd('.');
                    continue;
                }

                //Remove the last character if the last block would be followed by a broken day (#)
                if (line.Length > blocks[^1] && line[^(blocks[^1] + 1)] == '#')
                {
                    line = line[..^1].TrimEnd('.');
                    continue;
                }

                //Remove the end, and the last block, if it fits exactly and is forced to be there.
                if (line.Length > blocks[^1] && line[^blocks[^1]..].Contains('#') && line[^(blocks[^1] + 1)] == '.')
                {
                    line = line[..^(blocks[^1] + 1)].TrimEnd('.');
                    blocks.RemoveAt(blocks.Count - 1);
                    continue;
                }

                break;
            }

            int nLeeway = line.Length - blocks.Sum(x => x+1) + 1;

            if (nLeeway < 0)
            {
                return 0;
            }

            //Shortcut : Based on total length and holes and stuff, it's nonogram
            if (nLeeway == 0 || blocks.Count == 0)
            {
                if (DEBUG_LEVEL >= nLevel) Console.WriteLine($"{new string(' ', 2*nLevel)}1 = {line} {String.Join(",", blocks)}");
                int nStart = 0;
                foreach(var testBlock in blocks)
                {
                    if (line[nStart..(nStart+testBlock)].Contains('.')) //if a segment would contain ., kill it.
                        return 0;
                    else if (nStart + testBlock < line.Length && line[nStart + testBlock] == '#') //we'd strip a #, kill it.
                        return 0;

                    nStart += testBlock + 1;
                }
                
                return 1;
            }

            //Only thing left is to bruteforce the remaining ones I guess... or be wise about it, but that's out of reach.
            long nSum = 0;
            if (blocks.Count > 1)
            {
                List<int> newBlocks = blocks.ToList();
                newBlocks.RemoveAt(0);
                int newBlocksLength = newBlocks.Sum(x => x+1);

                for(int i = 0; i <= line.Length - newBlocksLength - blocks[0]; i++)
                {                            
                    if (!line[i..(i + blocks[0])].Contains('.') && line[i + blocks[0]] != '#')
                    {
                        var nRes = Nonosolve(line[(i + blocks[0] + 1)..], newBlocks.ToList(), nLevel + 1);
                        nSum += nRes;
                    }

                    if (line[i] == '#') //We can't ditch a #
                        break;
                }
            }
            else
            {
                for (int i = 0; i <= line.Length - blocks[0]; i++)
                {
                    if (line[..i].Contains('#'))// We've ditched a #, we can stop; 
                        break;

                    if (line[i..(i + blocks[0])].Contains('.')) // we're including a ., we can skip
                        continue;
                    
                    if (line[(i + blocks[0])..].Contains('#')) // we haven't reached the first #
                        continue;

                    nSum ++;
                }
            }

            if (DEBUG_LEVEL >= nLevel) Console.WriteLine($"{new string(' ', 2*nLevel)}{nSum} = {line} {String.Join(",", blocks)}");
            return nSum;
        }
    }
}
