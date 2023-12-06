using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day09Process
    {
        private static Day09 Day = new Day09();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day09
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In9.txt");
        private List<long> numbers = new List<long>();
        private long firstIntruder;
        public Day09()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                var isValid = numbers.Count > 25 ? IsValid(long.Parse(line)) : false;

                if (numbers.Count > 25 && !isValid && firstIntruder == 0)
                {
                    firstIntruder = int.Parse(line);
                }

                numbers.Add(long.Parse(line));

            }
        }
        private bool IsValid(long number)
        {
            for (var index = numbers.Count - 1; index >= numbers.Count - 25 + 1; index--)
            {
                var invalid = false;
                for (var index2 = index - 1; index2 >= numbers.Count - 25 && !invalid; index2--)
                {
                    invalid = (numbers[index] + numbers[index2] == number);
                }
                if (invalid)
                {
                    return true;
                }
            }
            return false;
        }

        public void Part1()
        {
            Console.WriteLine("Day 9: Encoding Error {0} (part1)", firstIntruder);
        }
        public void Part2()
        {
            var found = false;
            long sum = 0;
            long min = 0;
            long max = 0;
            for (var index = 1; index < numbers.Count && !found; index++)
            {
                sum = numbers[index];
                min = 999999999999999;
                max = 0;
                for (var index2 = index + 1; index2 < numbers.Count && !found && sum < firstIntruder; index2++)
                {
                    sum += numbers[index2];
                    if (sum == firstIntruder)
                    {
                        found = true;
                    }
                    if (numbers[index2] > max)
                    {
                        max = numbers[index2];

                    }
                    if (numbers[index2] < min)
                    {
                        min = numbers[index2];
                    }
                }

            }

            Console.WriteLine("Day 9: Encoding Error {0} (part2)", max + min);
        }
    }
}
