using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day03Process
    {
        private static Day03 Day = new Day03();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day03
    {
        private string[] map = new string[354];
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In3.txt");
        private int lines = 0;
        public Day03()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            var line_read = string.Empty;

            while ((line_read = file.ReadLine()) != null)
            {
                map[lines++] = line_read;
            }
        }
        public void Part1()
        {
            Console.WriteLine("Day 3: Toboggan Trajectory : {0}", helper(map, 1, 3, lines));
        }
        public void Part2()
        {
            Console.WriteLine("Day 3: Toboggan Trajectory : {0}", helper(map, 1, 1, lines) *
        helper(map, 1, 3, lines) *
        helper(map, 1, 5, lines) *
        helper(map, 1, 7, lines) *
        helper(map, 2, 1, lines));
        }
        private int helper(string[] map, int x, int y, int lines)
        {
            var trees = 0;
            var line = 0;
            var column = 0;

            for (; line < lines;)
            {
                if (map[line][column % map[line].Length] == '#')
                {
                    trees++;
                }
                line += x;
                column += y;
            }

            return trees;
        }
    }
}
