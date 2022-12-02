
namespace AdventOfCode2022.Implementations
{
    internal class Day1 :Base, IBase
    {
        private List<int> _reindeerCalories= new List<int>();

        public void Run()
        {
            ReadData();
            Part1();
            Part2();
        }
        public void Part1()
        {
            Display(resultPart1: _reindeerCalories.Last().ToString());
        }

        public void Part2()
        {
            Display(resultPart2: _reindeerCalories.TakeLast(3).Sum().ToString());
        }

        public void ReadData()
        {
            var iterator = 0;
            _reindeerCalories = streamReader.ReadToEnd().Split(Environment.NewLine).Select(u => new
            {
                index = string.IsNullOrEmpty(u) ? ++iterator : iterator,
                value = string.IsNullOrEmpty(u) ? 0: int.Parse(u)
            }).GroupBy(u=>u.index).Select(u=>u.Sum(e=>e.value)).OrderBy(u=>u).ToList();
             
        }



    }
}
