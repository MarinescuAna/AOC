using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day11 : BaseDay
    {
        private int[][] octopuses;
        public Day11() : base(11, "Dumbo Octopus")
        {
            octopuses = new int[10][];
            ReadInput();
        }
        private int CountFlashes()
        {
            return octopuses.Select(u => u.Where(u => u == 0).Count()).Sum();
        }
        private void IncreaseAndFlash(Stack<(int line, int column)> shouldFlash)
        {
            for (var i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    octopuses[i][j] = octopuses[i][j] >= 9 ? 0 : octopuses[i][j] + 1;
                    if (octopuses[i][j] == 0)
                    {
                        shouldFlash.Push((i, j));
                    }
                }
            }
        }
        private void IncreaseAdjacent((int line, int column) c, Stack<(int line, int column)> flash)
        {
            (int line, int column)[] coord = {
                (-1,-1),
                (-1,0),
                (-1,1),
                (0,1),
                (1,1),
                (1,0),
                (1,-1),
                (0,-1)
            };
            var auxLine = c.line;
            var auxColumn = c.column;

            foreach (var pair in coord)
            {
                auxLine = c.line + pair.line;
                auxColumn = c.column + pair.column;
                if (auxLine > -1 && auxLine < 10 && auxColumn > -1 && auxColumn < 10)
                {
                    if (octopuses[auxLine][auxColumn] == 9)
                    {
                        octopuses[auxLine][auxColumn] = 0;
                        flash.Push((auxLine, auxColumn));
                    }
                    else if (octopuses[auxLine][auxColumn] != 0)
                    {
                        octopuses[auxLine][auxColumn]++;
                    }
                }
            }
        }
        public override void Part1()
        {
            var flashes = 0;
            var shouldFlash = new Stack<(int line, int column)>();
            for (var step = 0; step < 100; step++)
            {
                IncreaseAndFlash(shouldFlash);
                if (shouldFlash.Count()!=0)
                {
                    while (shouldFlash.Count() != 0)
                    {
                        IncreaseAdjacent(shouldFlash.Pop(), shouldFlash);
                    }
                    flashes += CountFlashes();
                }
            }
            Display(flashes.ToString(), true);
        }

        public override void Part2()
        {
            var step = 100;
            var shouldFlash = new Stack<(int line, int column)>();
            for (; CountFlashes() != 100; step++)
            {
                IncreaseAndFlash(shouldFlash);
                if (shouldFlash.Count() != 0)
                {
                    while (shouldFlash.Count() != 0 || shouldFlash.Count()==100)
                    {
                        IncreaseAdjacent(shouldFlash.Pop(), shouldFlash);
                    }
                }
            }
            Display(step.ToString(), false);
        }

        public override void ReadInput()
        {
            for (var i = 0; (line = StreamReader.ReadLine()) != null; i++)
            {
                octopuses[i] = line.ToCharArray().Select(u => int.Parse(u.ToString())).ToArray();
            }
        }
    }
}
