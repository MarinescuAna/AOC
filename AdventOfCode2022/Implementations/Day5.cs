namespace AdventOfCode2022.Implementations
{
    internal class Day5 : Base, IBase
    {
        private List<string> _stacksOfCrates = new List<string>();
        private List<(int noCrates, int from, int to)> _rearrangementSteps = new List<(int noCrates, int from, int to)>();
        public void Part1()
        {
            var stacksOfCrates = new List<string>(_stacksOfCrates);
            _rearrangementSteps.ForEach(step => MoveCrate(stacksOfCrates,step));
            Display(resultPart1: new string(stacksOfCrates.Select(crate => crate.Last()).ToArray()));
        }

        public void Part2()
        {
            var stacksOfCrates = new List<string>(_stacksOfCrates);
            _rearrangementSteps.ForEach(step => MoveCrate(stacksOfCrates,step,false));
            Display(resultPart2: new string(stacksOfCrates.Select(crate => crate.Last()).ToArray()));
        }
        private void MoveCrate(List<string> stacksOfCrates,(int noCrates, int from, int to) procedure, bool changeOrgder = true)
        {
            var crateForMoving = changeOrgder? stacksOfCrates[procedure.from].Substring(stacksOfCrates[procedure.from].Length - procedure.noCrates - 1).Reverse().ToArray():
                 stacksOfCrates[procedure.from].Substring(stacksOfCrates[procedure.from].Length - procedure.noCrates - 1).ToArray();
            stacksOfCrates[procedure.from] = stacksOfCrates[procedure.from].Remove(stacksOfCrates[procedure.from].Length - procedure.noCrates - 1);
            stacksOfCrates[procedure.to]+= new string(crateForMoving);
        }
        public void ReadData()
        {
            var splitData = streamReader.ReadToEnd().Split("\r\n\r\n").ToList();
            _stacksOfCrates = splitData[0].Split(Environment.NewLine).Select(line =>line.Replace(" ","")).ToList();
            _rearrangementSteps = splitData[1].Split(Environment.NewLine).Select(line => {
                var cleanLine = line.Replace("move ", "").Replace("from ", "").Replace("to ", "").Split(' ');
                return (int.Parse(cleanLine[0])-1, int.Parse(cleanLine[1])-1, int.Parse(cleanLine[2])-1);
                }).ToList();
        }

        public void Run()
        {
           ReadData();
           Part1();
           Part2();
        }
    }
}
