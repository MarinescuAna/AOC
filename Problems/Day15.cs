using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day15: BaseDay
    {
        private List<List<int>> matrix = new List<List<int>>();
        private (int, int)[] coordonates = {(1,0),(0,1),(-1,0),(0,-1)};
        public Day15() : base(15, "")
        {
            ReadInput();
        }
        private bool IsValid(int line, int column)
        {
            return line >= 0 && line <= matrix.Count() - 1 && column >= 0 && column <= matrix[0].Count()-1;
        }

        private int Manhattan((int x, int y) p)
        {
            return matrix[0].Count() - 1 - p.x + matrix.Count() - 1 - p.y;
        }

        private List<(int,int)> Neighbors((int, int) current)
        {
            var result = new List<(int, int)>();
            foreach(var pair in coordonates)
            {
                result.Add((current.Item1+pair.Item1,current.Item2+pair.Item2));
            }
            return result.Where(u=>IsValid(u.Item1,u.Item2)).ToList();
        }
        private int LowestRisk()
        {
            var dest = (matrix[0].Count() - 1, matrix.Count() - 1);
            var candidates = new SortedSet<(int risk, int manhattan, int x, int y)>()
                {
                    (0, Manhattan((0, 0)), 0, 0)
                };
            var visited = new HashSet<(int, int)> { (0, 0) };

            while (candidates.Count > 0)
            {
                var currentCandidate = candidates.First();
                candidates.Remove(currentCandidate);
                var currentSpace = (currentCandidate.x, currentCandidate.y);

                foreach (var neighbor in Neighbors(currentSpace))
                {
                    if (neighbor == dest)
                    {
                        return currentCandidate.risk + matrix[dest.Item1][dest.Item2];
                    }
                    else if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        var next = (matrix[neighbor.Item1][neighbor.Item2] + currentCandidate.risk, Manhattan(neighbor), neighbor.Item1, neighbor.Item2);
                        candidates.Add(next);
                    }
                }
            }
            return -1;
        }
        public override void Part1()
        {
          //  Display(LowestRisk().ToString(), true);
        }

        public override void Part2()
        {
            var oldDimentionsLine = matrix.Count();
            var oldDimentionsColumns = matrix[0].Count();
            for(var step = 0; step < 4; step++)
            {
                for (var i = 0; i < oldDimentionsLine; i++)
                {
                    var newLine = new List<int>();
                    for (var j = 0; j < oldDimentionsColumns; j++)
                    {
                        newLine.Add(matrix[oldDimentionsLine * step + i][j] + 1 == 10 ? 1 : matrix[oldDimentionsLine * step + i][j] + 1);
                    }
                    matrix.Add(newLine);
                }
               
            }
            for (var step = 0; step < 4; step++)
            {
                for (var i = 0; i < oldDimentionsColumns; i++)
                {
                    for (var j = 0; j < matrix.Count(); j++)
                    {
                        matrix[j].Add(matrix[j][oldDimentionsLine * step + i] + 1 == 10 ? 1 : matrix[j][oldDimentionsLine * step + i] + 1);
                    
                    }

                }

            }
            Display(LowestRisk().ToString(), false);
        }
        public override void ReadInput()
        {
            matrix = StreamReader.ReadToEnd().Split(Environment.NewLine).Select(u => u.Select(t => int.Parse(t.ToString())).ToList()).ToList();
        }
    }
}
