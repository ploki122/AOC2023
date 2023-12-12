using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 10)]
    internal class Day10 : AoCDay
    {
        private readonly Dictionary<(char, char), char> bends = new()
        {
            { ('>', 'J'), '^'},
            { ('>', '-'), '>'},
            { ('>', '7'), 'v'},

            { ('^', '7'), '<'},
            { ('^', 'F'), '>'},
            { ('^', '|'), '^'},

            { ('<', 'F'), 'v'},
            { ('<', '-'), '<'},
            { ('<', 'L'), '^'},

            { ('v', 'J'), '<'},
            { ('v', '|'), 'v'},
            { ('v', 'L'), '>'}
        };
        private readonly Dictionary<char, (int, int)> movements = new()
        {
            {'>', (0,1)},
            {'^', (-1,0)},
            {'<', (0,-1)},
            {'v', (1,0)}
        };

        protected override string PartOne(ref string[] file)
        {
            (int, int) position = (0, 0);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains('S'))
                {
                    position = (i, file[i].IndexOf('S'));
                    break;
                }
            }

            char cursor = '\0';
            (int, int) nextCoords = (0, 0);
            foreach (var pathOption in new char[] { '>', '^', '<', 'v' })
            {
                (int, int) directions = movements[pathOption];
                nextCoords = (position.Item1 + directions.Item1, position.Item2 + directions.Item2);
                
                if (bends.ContainsKey((pathOption, file[nextCoords.Item1][nextCoords.Item2])))
                {
                    cursor = pathOption;
                    break;
                }
            }

            long nSum = 1;
            while (file[nextCoords.Item1][nextCoords.Item2] != 'S')
            {
                cursor = bends[(cursor, file[nextCoords.Item1][nextCoords.Item2])];
                (int, int) directions = movements[cursor];
                nextCoords = (nextCoords.Item1 + directions.Item1, nextCoords.Item2 + directions.Item2);
                nSum++;
            }

            return (nSum/2).ToString();
        }

        protected override string PartTwo(ref string[] file)
        {
            //find start
            (int, int) startPosition = (0, 0);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains('S'))
                {
                    startPosition = (i, file[i].IndexOf('S'));
                    break;
                }
            }

            //find initial direction
            char cursor = '\0';
            (int, int) nextCoords = (0, 0);
            foreach (var pathOption in new char[] { '>', '^', '<', 'v' })
            {
                (int, int) directions = movements[pathOption];
                nextCoords = (startPosition.Item1 + directions.Item1, startPosition.Item2 + directions.Item2);

                if (bends.ContainsKey((pathOption, file[nextCoords.Item1][nextCoords.Item2])))
                {
                    cursor = pathOption;
                    break;
                }
            }


            //find path
            char initialMove = cursor;
            List<(int, int)> path = new() { startPosition };
            while (nextCoords != startPosition)
            {
                path.Add(nextCoords);
                cursor = bends[(cursor, file[nextCoords.Item1][nextCoords.Item2])];
                (int, int) directions = movements[cursor];
                nextCoords = (nextCoords.Item1 + directions.Item1, nextCoords.Item2 + directions.Item2);
            }

            foreach(var bend in bends)
            {
                if (bend.Key.Item1 == cursor && bend.Value == initialMove)
                {
                    file[startPosition.Item1] = file[startPosition.Item1][..startPosition.Item2] + bend.Key.Item2 + file[startPosition.Item1][(startPosition.Item2 + 1)..];
                    break;
                }
            }

            //replace everything that's not the path with '?'
            for (int y = 0; y < file.Length; y++)
            {
                for (int x = 0; x < file[y].Length; x++)
                {
                    if (!path.Contains((y, x)))
                    {
                        file[y] = file[y][..x] + "?" + file[y][(x + 1)..];
                    }
                }
            }

            long nSum = 0;
            //tag the rest of the node : Position -1 is always outside, obviously, and then we have to find a magical way to solve this shit.
            foreach (var line in file)
            {
                bool state = false;
                string newLine = "";
                char lastMeaningful = '\0';

                for (int x = 0; x<line.Length; x++)
                {
                    if (line[x] == '?')
                        if (state)
                        {
                            newLine += 'I';
                            nSum++;
                        }
                        else
                            newLine += 'O';
                    else
                        newLine += line[x];

                    switch(line[x])
                    {
                        case '-': //can ignore
                            continue;

                        case '|': //vertical wall = flip state
                            state = !state;
                            break;

                        case 'F': //We'll deal with those later
                        case 'L':
                            lastMeaningful = line[x];
                            break;

                        case 'J': //only a wall if "next" to a F
                            if (lastMeaningful == 'F')
                                state = !state;
                            break;

                        case '7': //only a wall if "next" to a L
                            if (lastMeaningful == 'L')
                                state = !state;
                            break;
                    }
                }
            }

            //return, at long last
            return nSum.ToString(); //1312850
        }
    }
}
