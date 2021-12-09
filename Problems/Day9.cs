using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day9 : BaseDay
    {
        private List<int[]> Heightmap;
        public Day9() : base(9, "Smoke Basin")
        {
            Heightmap = new List<int[]>();
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

                        sum += (Heightmap[i][j] + 1);
                    }
                }
            }
            Display(sum.ToString(), true);
        }

        private int DiscoverBasins(int line, int column)
        {
            var size = 0;
            while (Heightmap[line][column]!=9)
            {
                Heightmap[line][column] = 9;
                size++;
                column++;
                if (column == Heightmap[line].Length || Heightmap[line][column]==9)
                {
                    column = 0;
                    line++;
                    if (line == Heightmap.Count)
                    {
                        break;
                    }
                }
            }
            return size;

        }

        public void DisplayMatrix()
        {
            
            for (var i = 0; i < Heightmap.Count; i++)
            {
                Console.WriteLine();
                for (var j = 0; j < Heightmap[i].Length; j++)
                {
                    Console.Write(Heightmap[i][j]);
                }
            }
        }
        public override void Part2()
        {
            var sum = 0l;
            for (var i = 0; i < Heightmap.Count; i++)
            {
                for (var j = 0; j < Heightmap[i].Length; j++)
                {
                    if (Heightmap[i][j] != 9)
                    {
                        Console.WriteLine(DiscoverBasins(i, j));
                        DisplayMatrix();
                    }
                }
            }
        }
        public override void ReadInput()
        {
            while ((line = StreamReader.ReadLine()) != null)
            {
                Heightmap.Add(line.Trim().Select(u => int.Parse(u.ToString())).ToArray());
            }
        }
    }
}
