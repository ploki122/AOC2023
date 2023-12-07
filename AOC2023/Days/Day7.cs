using AOC2023.Framework;

namespace AOC2023.Days
{
    [AoC(2023, 7)]
    internal class Day7 : AoCDay
    {
        protected override string PartOne(ref string[] file)
        {
            file = file.Order(new CamelHandSorter()).ToArray();
            long nSum = 0;
            for (int handNo = 0; handNo < file.Length; handNo++)
            {
                nSum += (handNo + 1) * int.Parse(file[handNo].Split(" ")[1]);
            }

            return nSum.ToString(); //252656917
        }

        protected override string PartTwo(ref string[] file)
        {
            file = file.Order(new CamelHandSorter(false)).ToArray();
            long nSum = 0;
            for (int handNo = 0; handNo < file.Length; handNo++)
            {
                Console.WriteLine($"{handNo + 1} {file[handNo]}");
                nSum += (handNo + 1) * int.Parse(file[handNo].Split(" ")[1]);
            }

            return nSum.ToString(); //253499763
        }

        private class CamelHandSorter : IComparer<string>
        {
            readonly bool partTwo;
            public CamelHandSorter(bool partOne = true)
            {
                partTwo = !partOne;
            }

            public int Compare(string? x, string? y)
            {
                return CamelHandPower(x, partTwo) - CamelHandPower(y, partTwo);
            }
        }

        internal static int CamelHandPower(string? line, bool useWildCards)
        {
            if (line == null) return -1;
            string hand = line.Split(" ")[0];

            //5
            //4
            //3+2
            //3
            //2+2
            //2
            //1

            if (hand == null || hand.Length == 0)
            {
                return -1;
            }

            Dictionary<char, int> cards = new();
            foreach (var card in hand)
            {
                if (!cards.ContainsKey(card))
                    cards[card] = 0;

                cards[card]++;
            }

            Dictionary<int, List<char>> reverseCards = new()
            {
                { 1, new List<char>() },
                { 2, new List<char>() },
                { 3, new List<char>() },
                { 4, new List<char>() },
                { 5, new List<char>() }
            };

            int nbWildcards = 0;
            if (useWildCards)
            {
                if (cards.ContainsKey('J'))
                    nbWildcards = cards['J'];

                cards.Remove('J');
            }

            foreach (var keyValue in cards)
            {
                reverseCards[keyValue.Value].Add(keyValue.Key);
            }

            int handDiscriminant = 
                CardPower(hand[0], useWildCards) * 16 * 16 * 16 * 16 
                + CardPower(hand[1], useWildCards) * 16 * 16 * 16 
                + CardPower(hand[2], useWildCards) * 16 * 16 
                + CardPower(hand[3], useWildCards) * 16 
                + CardPower(hand[4], useWildCards);

            if (reverseCards[5].Count > 0 || nbWildcards == 5 || reverseCards[5 - nbWildcards].Count > 0)
                return 7000000 + handDiscriminant;
            else if (reverseCards[4].Count > 0 || reverseCards[4 - nbWildcards].Count > 0)
                return 6000000 + handDiscriminant;
            else if ((reverseCards[3].Count > 0 && reverseCards[2].Count > 0) || (reverseCards[2].Count > 1 && nbWildcards == 1))
                return 5000000 + handDiscriminant;
            else if (reverseCards[3].Count > 0 || reverseCards[3 - nbWildcards].Count > 0)
                return 4000000 + handDiscriminant;
            else if (reverseCards[2].Count > 1)
                return 3000000 + handDiscriminant;
            else if (reverseCards[2].Count > 0 || reverseCards[2 - nbWildcards].Count > 0)
                return 2000000 + handDiscriminant;
            else
                return 1000000 + handDiscriminant;
        }

        internal static int CardPower(char card, bool useWildcards = false)
        {
            switch (char.ToUpper(card))
            {
                case 'A': return 14;
                case 'K': return 13;
                case 'Q': return 12;
                case 'J': if (useWildcards) return 0; else return 11;
                case 'T': return 10;
                default: return int.Parse(card.ToString());
            }
        }
    }
}
