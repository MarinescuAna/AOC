using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Implementations
{
    internal class Day9 : Base, IBase
    {
        private List<(char direction, int steps)> _seriesOfMotions = new List<(char direction, int steps)>();
        public void Part1()
        {
            var visited = new Dictionary<(int i, int j), int>();
            (int xH, int yH, int xT, int yT) currentPosition = (0, 0, 0 , 0);

            visited.Add((0, 0), 1);

            _seriesOfMotions.ForEach(motion =>
            {
                 currentPosition = motion.direction switch 
                {
                   'U' => GoUp(motion.steps, visited, currentPosition),
                   'R' => GoRight(motion.steps, visited, currentPosition),
                };
            });

            Display(resultPart1: visited.Count().ToString());
        }
        private void AddPosition(Dictionary<(int i, int j), int> visited, (int x, int y) currentPozTail)
        {
            if (visited.ContainsKey(currentPozTail))
            {
                visited[currentPozTail]++;
            }
            else
            {
                visited.Add(currentPozTail, 1);
            }
        }
        private (int xH, int yH, int xT, int yT) GoUp(int steps, Dictionary<(int i, int j), int> visited, (int xH, int yH, int xT, int yT) currentPoz)
        {
            for(var i = 0; i < steps; i++)
            {
                currentPoz.xH++;
                if(currentPoz.yT!= currentPoz.yH)
                {
                    currentPoz.yT = currentPoz.yH;
                }
                currentPoz.xT++;
                AddPosition(visited, (currentPoz.xT, currentPoz.yT));
            }
            return currentPoz;
        }
        private (int xH, int yH, int xT, int yT) GoRight(int steps, Dictionary<(int i, int j), int> visited, (int xH, int yH, int xT, int yT) currentPoz)
        {
            for (var i = 0; i < steps; i++)
            {
                currentPoz.yH++;
                if (currentPoz.xT != currentPoz.xH)
                {
                    currentPoz.xT = currentPoz.xH;
                }
                currentPoz.yT++;
                AddPosition(visited, (currentPoz.xT, currentPoz.yT));
            }
            return currentPoz;
        }
        public void Part2()
        {
            throw new NotImplementedException();
        }

        public void ReadData()
        {
            _seriesOfMotions = streamReader.ReadToEnd().Split(Environment.NewLine).Select(line => (line.Split(" ")[0][0], int.Parse(line.Split(" ")[1]))).ToList();
        }

        public void Run()
        {
            ReadData();
            Part1();
        }
    }
}
