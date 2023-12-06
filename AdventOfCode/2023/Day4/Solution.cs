using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day4
{
    public class Solution : Base
    {
        private void BothParts()
        {
            var cards = streamReader.ReadToEnd().Split(Environment.NewLine).Select(u => u.Split(":")[1]);
            var points = 0;
            var scratchcards = new (int matches, int copies)[cards.Count()];
            var index = 0;
            foreach (var card in cards)
            {
                var winningList = Regex.Matches(card.Split('|')[0], "\\d+").Select(x => x.Value);
                var othersList = Regex.Matches(card.Split('|')[1], "\\d+").Select(x => x.Value);

                var partialSum = 0;
                var intersection = winningList.Intersect(othersList);

                foreach (var value in intersection)
                    partialSum = partialSum == 0 ? 1 : partialSum * 2;

                points += partialSum;
                scratchcards[index++]=(intersection.Count(), 1);
            }

            Console.WriteLine(points);

            for (var i = 0; i < scratchcards.Length; i++)
            {
                if (scratchcards[i].matches > 0)
                {
                    Enumerable.Range(i + 1, scratchcards[i].matches).ToList().ForEach(x => scratchcards[x].copies += scratchcards[i].copies);
                }
            }

            Console.WriteLine(scratchcards.Sum(x => x.copies));
        }
        public override void Run()
        {
            BothParts();
        }
    }
}
