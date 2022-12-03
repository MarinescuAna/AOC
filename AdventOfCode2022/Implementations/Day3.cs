namespace AdventOfCode2022.Implementations
{
    internal class Day3 : Base, IBase
    {
        private List<string> rucksacks = new List<string>();
        public void Part1()
        {
            Display(resultPart1: rucksacks.Sum(rucksack => GetPriority(rucksack.Substring(0, rucksack.Length / 2).Intersect(rucksack.Substring(rucksack.Length / 2)).First())).ToString());
        }
        public void Part2()
        {
            Display(resultPart2: rucksacks.Chunk(3).Sum(g => GetPriority(g[0].Intersect(g[1]).Intersect(g[2]).First())).ToString());
        }

        public void ReadData()
        {
            rucksacks = streamReader.ReadToEnd().Split(Environment.NewLine).ToList();
        }
        private int GetPriority(char ch) => ch - (Char.IsLower(ch) ? 'a' : 'A') + 1 + (Char.IsLower(ch) ? 0 : 26);

        public void Run()
        {
            ReadData();
            Part1();
            Part2();
        }
    }
}
