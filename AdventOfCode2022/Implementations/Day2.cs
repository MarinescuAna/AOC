namespace AdventOfCode2022.Implementations
{
    internal class Day2 : Base, IBase
    {
        private List<string> _rounds= new List<string>();
        public void Part1()
        {
            Display(resultPart1: _rounds.Select(u =>  u switch
            {
                "A X" => 4,
                "A Y" => 8,
                "A Z" => 3,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 7,
                "C Y" => 2,
                "C Z" => 6,
                _ => 0
            }).Sum().ToString());
        }

        public void Part2()
        {
            Display(resultPart2: _rounds.Select(u => u switch
            {
                "A X" => 3,
                "A Y" => 4,
                "A Z" => 8,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 2,
                "C Y" => 6,
                "C Z" => 7,
                _ => 0
            }).Sum().ToString());
        }

        public void ReadData()
        {
            _rounds = streamReader.ReadToEnd().Split(Environment.NewLine).ToList();
        }

        public void Run()
        {
            ReadData();
            Part1();
            Part2();
        }
    }
}
