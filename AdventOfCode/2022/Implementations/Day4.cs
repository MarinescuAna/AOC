namespace AdventOfCode2022.Implementations
{
    internal class Day4 : Base, IBase
    {
        private List<string> _sectionAssignments = new List<string>();
        public void Part1()
        {
            Display(resultPart1: _sectionAssignments.Sum(line => InRange(line.Split(",")[0], line.Split(",")[1],1)).ToString());
        }

        private int InRange(string pair1, string pair2, int part) => part switch
        {
            1 => NumberIsInPair(int.Parse(pair1.Split("-")[0]), int.Parse(pair1.Split("-")[1]), int.Parse(pair2.Split("-")[0]), int.Parse(pair2.Split("-")[1])) ||
                PairIsInPair(int.Parse(pair1.Split("-")[0]), int.Parse(pair1.Split("-")[1]), int.Parse(pair2.Split("-")[0]), int.Parse(pair2.Split("-")[1]))
                ? 1 : 0,
            2 => NoOverlap(int.Parse(pair1.Split("-")[0]), int.Parse(pair1.Split("-")[1]), int.Parse(pair2.Split("-")[0]), int.Parse(pair2.Split("-")[1]))? 0:1,
            _ => 0
        };
        private bool PairIsInPair(int pair1X, int pair1Y, int pair2X, int pair2Y) =>
            (pair1X >= pair2X && pair1Y <= pair2Y) || (pair1X <= pair2X && pair1Y >= pair2Y);
        private bool NumberIsInPair(int pair1X, int pair1Y, int pair2X, int pair2Y) =>
            (pair1X == pair1Y && (pair1X - pair2Y) * (pair1X - pair2X) <= 0) || (pair2X == pair2Y && (pair2X - pair1Y) * (pair2X - pair1X) <= 0) ;
        public void Part2()
        {
            Display(resultPart2: _sectionAssignments.Sum(line => InRange(line.Split(",")[0], line.Split(",")[1],2)).ToString());
        }
        private bool NoOverlap(int pair1X, int pair1Y, int pair2X, int pair2Y) =>
            pair1Y < pair2X || //pair1 is in the pair2 left
            pair1X > pair2Y || //pair1 is in the pair2 right
            pair2Y < pair1X || //pair2 is in the pair1 left
            pair2X > pair1Y; //pair2 is in the pair1 right
        public void ReadData()
        {
            _sectionAssignments = streamReader.ReadToEnd().Split(Environment.NewLine).ToList();
        }
        public void Run()
        {
            ReadData();
            Part1();
            Part2();
        }
    }
}
