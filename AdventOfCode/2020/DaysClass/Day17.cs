using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day17Process
    {
        private static Day17 Day17 = new Day17();
        public static void Process()
        {
            Day17.Part1();
            Day17.Part2();
        }
    }
    public class Day17
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In17.txt");
        private List<(int, int, int)> ActiveCubes3D = new List<(int, int, int)>();
        private List<(int, int, int,int)> ActiveCubes4D = new List<(int, int, int,int)>();
        private List<(int, int, int)> Positions2D = new List<(int, int, int)> {
        (1, 1,  0), (1, 0,  0), (1, -1,  0), (0, -1,  0), (-1, -1,  0), (-1, 0,  0), (-1, 1,  0), (0, 1,  0),
            (1, 1,  1), (1, 0,  1), (1, -1,  1), (0, -1,  1), (-1, -1,  1), (-1, 0,  1), (-1, 1,  1), (0, 1,  1), (0, 0,  1),
            (1, 1, -1), (1, 0, -1), (1, -1, -1), (0, -1, -1), (-1, -1, -1), (-1, 0, -1), (-1, 1, -1), (0, 1, -1), (0, 0, -1)
        };
        private List<(int, int, int, int)> Positions4D = new List<(int, int, int, int)> {
        (1, 1, 0, 0), (1, 0, 0, 0), (1, -1, 0, 0), (0, -1, 0, 0), (-1, -1, 0, 0), (-1, 0, 0, 0), (-1, 1, 0, 0), (0, 1, 0, 0), (0, 0, 1, 0), (1, 1, 1, 0), (1, 0, 1, 0), (1, -1, 1, 0), (0, -1, 1, 0), (-1, -1, 1, 0), (-1, 0, 1, 0), (-1, 1, 1, 0), (0, 1, 1, 0), (0, 0, -1, 0), (1, 1, -1, 0), (1, 0, -1, 0), (1, -1, -1, 0), (0, -1, -1, 0), (-1, -1, -1, 0), (-1, 0, -1, 0), (-1, 1, -1, 0), (0, 1, -1, 0),
            (0, 0, 0, 1), (1, 1, 0, 1), (1, 0, 0, 1), (1, -1, 0, 1), (0, -1, 0, 1), (-1, -1, 0, 1), (-1, 0, 0, 1), (-1, 1, 0, 1), (0, 1, 0, 1), (0, 0, 1, 1), (1, 1, 1, 1), (1, 0, 1, 1), (1, -1, 1, 1), (0, -1, 1, 1), (-1, -1, 1, 1), (-1, 0, 1, 1), (-1, 1, 1, 1), (0, 1, 1, 1), (0, 0, -1, 1), (1, 1, -1, 1), (1, 0, -1, 1), (1, -1, -1, 1), (0, -1, -1, 1), (-1, -1, -1, 1), (-1, 0, -1, 1), (-1, 1, -1, 1),
            (0, 1, -1, 1), (0, 0, 0, -1), (1, 1, 0, -1), (1, 0, 0, -1), (1, -1, 0, -1), (0, -1, 0, -1), (-1, -1, 0, -1), (-1, 0, 0, -1), (-1, 1, 0, -1), (0, 1, 0, -1), (0, 0, 1, -1), (1, 1, 1, -1), (1, 0, 1, -1), (1, -1, 1, -1), (0, -1, 1, -1), (-1, -1, 1, -1), (-1, 0, 1, -1), (-1, 1, 1, -1), (0, 1, 1, -1), (0, 0, -1, -1), (1, 1, -1, -1), (1, 0, -1, -1), (1, -1, -1, -1), (0, -1, -1, -1), (-1, -1, -1, -1), (-1, 0, -1, -1), (-1, 1, -1, -1), (0, 1, -1, -1)
         };
        public Day17()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            var line = string.Empty;
            var lineIndex = 0;
            while (!string.IsNullOrEmpty(line = file.ReadLine()))
            {
                for (var col = 0; col < line.Length; col++)
                {
                    if (line[col] == '#')
                    {
                        ActiveCubes3D.Add((lineIndex, col, 0));
                        ActiveCubes4D.Add((lineIndex, col, 0,0));
                    }
                }
                lineIndex++;
            }
        }
        public void Part1()
        {
            var activeCubesTemporary = new List<(int, int, int)>();
            var Neightbours = new Dictionary<(int, int, int), int>();

            for (var cycle = 0; cycle < 6; cycle++)
            {
                Neightbours.Clear();
                foreach (var cub in ActiveCubes3D)
                {
                    FoundNeightbours3D(cub, Neightbours);
                }

                activeCubesTemporary.Clear();
                foreach (var (cube, count) in Neightbours)
                {
                    if (count == 2 && ActiveCubes3D.Contains(cube))
                    {
                        activeCubesTemporary.Add(cube);
                    }
                    else if (count == 3)
                    {
                        activeCubesTemporary.Add(cube);
                    }
                }


                var swap = ActiveCubes3D;
                ActiveCubes3D = activeCubesTemporary;
                activeCubesTemporary = swap;
            }

            Console.WriteLine($"Day 17: Conway Cubes {ActiveCubes3D.Count} (part1)");
        }
        private void FoundNeightbours3D((int x, int y, int z) cube, Dictionary<(int, int, int), int> Neightbours)
        {
            foreach (var (x, y, z) in Positions2D)
            {
                var pos = (cube.x + x, cube.y + y, cube.z + z);
                if (Neightbours.ContainsKey(pos))
                {
                    Neightbours[pos]++;
                }
                else
                {
                    Neightbours[pos] = 1;
                }
            }
        }
        private void FoundNeightbours4D((int x, int y, int z, int w) cube, Dictionary<(int, int, int, int), int> Neightbours)
        {
            foreach (var (x, y, z, w) in Positions4D)
            {
                var pos = (cube.x + x, cube.y + y, cube.z + z, cube.w + w);
                if (Neightbours.ContainsKey(pos))
                {
                    Neightbours[pos]++;
                }
                else
                {
                    Neightbours[pos] = 1;
                }
            }
        }
        public void Part2()
        {
            var activeCubesTemporary = new List<(int, int, int, int)>();
            var Neightbours = new Dictionary<(int, int, int, int), int>();

            for (var cycle = 0; cycle < 6; cycle++)
            {
                Neightbours.Clear();
                foreach (var cub in ActiveCubes4D)
                {
                    FoundNeightbours4D(cub, Neightbours);
                }

                activeCubesTemporary.Clear();
                foreach (var (cube, count) in Neightbours)
                {
                    if (count == 2 && ActiveCubes4D.Contains(cube))
                    {
                        activeCubesTemporary.Add(cube);
                    }
                    else if (count == 3)
                    {
                        activeCubesTemporary.Add(cube);
                    }
                }


                var swap = ActiveCubes4D;
                ActiveCubes4D = activeCubesTemporary;
                activeCubesTemporary = swap;
            }

            Console.WriteLine($"Day 17: Conway Cubes {ActiveCubes4D.Count} (part2)");
        }
    }
}
