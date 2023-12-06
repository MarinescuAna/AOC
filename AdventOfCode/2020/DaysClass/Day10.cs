using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day10Process
    {
        private static Day10 Day = new Day10();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day10
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In10.txt");
        private List<int> adapters = new List<int>();

        public Day10()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            adapters.Add(0);
            while ((line = file.ReadLine()) != null)
            {
                adapters.Add(int.Parse(line));
            }

            adapters.Sort();
            adapters.Add(adapters[adapters.Count - 1] + 3);
        }

        public void Part1()
        {
            var frequnecyJolt = new int[5];
            for (var index = 0; index < adapters.Count - 1; index++)
            {

                frequnecyJolt[adapters[index + 1] - adapters[index]]++;
            }
            Console.WriteLine("Day 10: Adapter Array {0} (part1)", frequnecyJolt[1] * frequnecyJolt[3]);

        }
        public void Part2()
        {
            var frequency = new long[200];
            frequency[adapters.Count - 1] = 1;

            for (var index = adapters.Count - 2; index >= 0; index--)
            {
                long currentCount = 0;
                for (var connected = index + 1; connected < adapters.Count && adapters[connected] - adapters[index] <= 3; connected++)
                {
                    currentCount += frequency[connected];
                }
                frequency[index] = currentCount;
            }

            Console.WriteLine("Day 10: Adapter Array {0} (part2)", frequency[0]);
        }

    }
}
