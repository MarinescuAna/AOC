using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day15Process
    {
        private static Day15 Day15 = new Day15();
        public static void Process()
        {
            Day15.Part1();
            Day15.Part2();
        }
    }
    public class Day15
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In15.txt");
        private Dictionary<long, (long, long)> numbersSpoken = new Dictionary<long, (long, long)>();
        private long turn = 1;
        private long lastSpoke = 0;
        public Day15()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = file.ReadLine();

            foreach (var number in line.Split(','))
            {
                numbersSpoken.Add(long.Parse(number), ((long, long))(turn++, -1));
                lastSpoke = long.Parse(number);
            }

        }

        private long FindTheLast(long finish)
        {
            while (turn <= finish)
            {
                if (numbersSpoken.ContainsKey(lastSpoke))
                {
                    //var temp = lastSpoke;
                    if (numbersSpoken[lastSpoke].Item2 == -1)
                    {
                        lastSpoke = 0;
                    }
                    else
                    {
                        lastSpoke = numbersSpoken[lastSpoke].Item1 - numbersSpoken[lastSpoke].Item2;
                    }
                    if (numbersSpoken.ContainsKey(lastSpoke))
                    {
                        (long, long) pair = (turn, numbersSpoken[lastSpoke].Item1);
                        numbersSpoken[lastSpoke] = pair;
                    }
                    else
                    {
                        numbersSpoken.Add(lastSpoke, (turn, -1));
                    }
                }
                turn++;
            }

            return lastSpoke;
        }

        public void Part1()
        {
            Console.WriteLine("Day 15: Rambunctious Recitation {0} (part1)", FindTheLast(2020));
        }
        public void Part2()
        {

            Console.WriteLine("Day 15: Rambunctious Recitation {0} (part2)", FindTheLast(30000000));
        }
    }
}
