using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day13Process
    {
        private static Day13 Day13 = new Day13();
        public static void Process()
        {
            Day13.Part1();
            Day13.Part2();
        }
    }
    public class Day13
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In13.txt");
        private List<(long, int)> bus = new List<(long, int)>();
        private long myTimestamp = 0;
        private long start = 600000000000000;
        public Day13()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            myTimestamp = long.Parse(file.ReadLine());
            var index = 0;
            foreach (var number in file.ReadLine().Split(','))
            {
                if (number != "x")
                {
                    bus.Add((long.Parse(number), index));
                }
                index++;
            }
        }

        public void Part1()
        {
            long found = 0;
            var index = myTimestamp;
            for (; found == 0; index++)
            {
                foreach (var id in bus)
                {
                    if (index % id.Item1 == 0)
                    {
                        found = id.Item1;
                        break;
                    }

                }
            }

            index--;
            Console.WriteLine("Day 13: Seating System {0} (part1)", (index - myTimestamp) * found);

        }
        public void Part2()
        {
            var time = start;
            var continu = true;
            while (time % bus[0].Item1 != 0) time++;
            
            for (; continu; time += bus[0].Item1)
            {
                continu = false;
                foreach (var id in bus)
                {
                    if ((time + id.Item2) % id.Item1 != 0)
                    {
                        continu = true;
                        break;
                    }

                }

            }
            Console.WriteLine("Day 13: Seating System {0} (part2)", time - bus[0].Item1);
        }

    }
}
