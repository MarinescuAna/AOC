namespace AdventOfCode2023.Day7
{
    enum CardType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfKind,
        FullHouse,
        FourOfKind,
        FiveOfKind,
    }
    public class Solution : Base
    {
        private CardType GetCardType(string card, bool haveJ = false)
        {
            List< (char Character, int Count)> characters = card.GroupBy(c => c).Select(g => (g.Key, g.Count())).ToList();

            if (haveJ && card.Contains('J'))
            {
                (char,int) jElement = characters.First(a => a.Character == 'J');
                characters.Remove(jElement);
                var maxElement = ('a', 0);
                characters.ForEach(v => maxElement = v.Count > maxElement.Item2 ? (v.Character,v.Count) : maxElement);
                characters.Remove(maxElement);
                characters.Add((maxElement.Item1, maxElement.Item2 + jElement.Item2));
            }

            if (characters.Any(c => c.Count == 5))
                return CardType.FiveOfKind;
            else if (characters.Any(c => c.Count == 4))
                return CardType.FourOfKind;
            else if (characters.Count == 2 && characters.Any(c => c.Count == 3))
                return CardType.FullHouse;
            else if (characters.Any(c => c.Count == 3))
                return CardType.ThreeOfKind;
            else if (characters.Count(c => c.Count == 2) == 2)
                return CardType.TwoPair;
            else if (characters.Any(c => c.Count == 2))
                return CardType.OnePair;
            else
                return CardType.HighCard;
        }
        private int SecondCompare(string x, string y, string labels)
        {
            var i = 0;
            while (i < 5 && x[i] == y[i]) i++;
            return i == 5 ? 0 : labels.IndexOf(x[i]).CompareTo(labels.IndexOf(y[i]));
        }
        private int Compare((string, int, CardType) x, (string, int, CardType) y, string labels)
            => x.Item3 != y.Item3 ? ((int)x.Item3).CompareTo((int)y.Item3) : SecondCompare(x.Item1, y.Item1, labels);
        private long ComputeSum(List<string> input, string labels, bool haveJ = false)
        {
            var hands = input.Select(s => (card: s.Split(" ")[0], bid: int.Parse(s.Split(" ")[1]), type: GetCardType(s.Split(" ")[0],haveJ))).ToList();
            hands.Sort((x, y) => Compare(x, y, labels));

            return hands.Select((hand, index) => hand.bid * (index + 1)).Sum();
        }
        public override void Run()
        {
            var hands = streamReader.ReadToEnd().Split('\n').ToList();
            var labelsPart1 = "23456789TJQKA";
            var labelsPart2 = "J23456789TQKA";

            Console.WriteLine(ComputeSum(hands, labelsPart1));
            Console.WriteLine(ComputeSum(hands, labelsPart2,true));
        }
    }
}
