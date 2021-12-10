using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day9 : BaseDay
    {
        private List<int[]> Heightmap;
        private List<(int i, int j)> lowerPoints;
        public Day9() : base(9, "Smoke Basin")
        {
            Heightmap = new List<int[]>();
            lowerPoints = new List<(int i, int j)>();
            ReadInput();
        }
        private bool IsGreater(int line, int column)
        {
            if (line == 0)
            {
                if (column == 0)
                {
                    if (
                    Heightmap[line][column] < Heightmap[line + 1][column] &&
                    Heightmap[line][column] < Heightmap[line][column + 1]
                    )
                    {
                        return true;
                    }
                    return false;
                }
                else if (column == Heightmap[line].Length - 1)
                {
                    if (
                        Heightmap[line][column] < Heightmap[line + 1][column] &&
                        Heightmap[line][column] < Heightmap[line][column - 1]
                        )
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (
                    Heightmap[line][column] < Heightmap[line + 1][column] &&
                    Heightmap[line][column] < Heightmap[line][column - 1] &&
                    Heightmap[line][column] < Heightmap[line][column + 1]
                    )
                    {
                        return true;
                    }
                    return false;
                }
            }
            else if (line == Heightmap.Count - 1)
            {
                if (column == 0)
                {
                    if (
                   Heightmap[line][column] < Heightmap[line - 1][column] &&
                   Heightmap[line][column] < Heightmap[line][column + 1]
                   )
                    {
                        return true;
                    }
                    return false;
                }
                else if (column == Heightmap[line].Length)
                {
                    if (
                   Heightmap[line][column] < Heightmap[line - 1][column] &&
                   Heightmap[line][column] < Heightmap[line][column - 1]
                   )
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (
                   Heightmap[line][column] < Heightmap[line - 1][column] &&
                   Heightmap[line][column] < Heightmap[line][column - 1] &&
                   Heightmap[line][column] < Heightmap[line][column + 1]
                   )
                    {
                        return true;
                    }
                    return false;
                }
            }
            else if (column == 0)
            {
                if (
                    Heightmap[line][column] < Heightmap[line - 1][column] &&
                    Heightmap[line][column] < Heightmap[line + 1][column] &&
                    Heightmap[line][column] < Heightmap[line][column + 1]
                    )
                {
                    return true;
                }
                return false;

            }
            else if (column == Heightmap[line].Length - 1)
            {
                if (
                        Heightmap[line][column] < Heightmap[line - 1][column] &&
                        Heightmap[line][column] < Heightmap[line + 1][column] &&
                        Heightmap[line][column] < Heightmap[line][column - 1]
                        )
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (
                    Heightmap[line][column] < Heightmap[line - 1][column] &&
                    Heightmap[line][column] < Heightmap[line + 1][column] &&
                    Heightmap[line][column] < Heightmap[line][column - 1] &&
                    Heightmap[line][column] < Heightmap[line][column + 1]
                    )
                {
                    return true;
                }
                return false;
            }
        }
        public override void Part1()
        {
            var sum = 0;
            for (var i = 0; i < Heightmap.Count; i++)
            {
                for (var j = 0; j < Heightmap[i].Length; j++)
                {
                    if (IsGreater(i, j))
                    {
                        lowerPoints.Add((i, j));
                        sum += (Heightmap[i][j] + 1);
                    }
                }
            }
            Display(sum.ToString(), true);
        }

        private int DiscoverBasins(int line, int column)
        {
            if (line < 0 || column < 0 || line > Heightmap.Count - 1 || column > Heightmap[0].Length - 1 || Heightmap[line][column] == 9)
            {
                return 0;
            }

            Heightmap[line][column] = 9;
            return 1 + DiscoverBasins(line, column + 1) + DiscoverBasins(line, column - 1) + DiscoverBasins(line + 1, column) + DiscoverBasins(line - 1, column);
        }
        public override void Part2()
        {
            var sizes = new List<int>();
            foreach (var point in lowerPoints)
            {
                sizes.Add(DiscoverBasins(point.i, point.j));
            }
            sizes.Sort();
            Display((sizes[sizes.Count - 1] * sizes[sizes.Count - 2] * sizes[sizes.Count - 3]).ToString(), false);
        }
        public override void ReadInput()
        {
            Heightmap = StreamReader.ReadToEnd().Split(Environment.NewLine).Select(u => u.Trim().Select(u => int.Parse(u.ToString())).ToArray()).ToList();
        }
    }
}
