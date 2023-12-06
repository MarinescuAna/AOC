using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day23Process
    {
        private static Day23 Day = new Day23();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }

    public class Day23
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In23.txt");
        private List<int> numbers = new List<int>();
        public Day23()
        {
            ReadInput();
        }


        private void ReadInput()
        {
            var line = string.Empty;
            line = file.ReadLine();
            foreach (var digit in line)
            {
                numbers.Add(int.Parse(digit.ToString()));
            }
        }
        private (int, int, int) PickUp(int currentPos)
        {
            if (currentPos == numbers.Count - 1)
            {
                var tuple = (numbers[0], numbers[1], numbers[2]);
                numbers.Remove(tuple.Item1);
                numbers.Remove(tuple.Item2);
                numbers.Remove(tuple.Item3);
                return tuple;
            }
            if (currentPos == numbers.Count - 2)
            {
                var tuple = (numbers[numbers.Count - 1], numbers[0], numbers[1]);
                numbers.Remove(tuple.Item1);
                numbers.Remove(tuple.Item2);
                numbers.Remove(tuple.Item3);
                return tuple;
            }
            if (currentPos == numbers.Count - 3)
            {
                var tuple = (numbers[numbers.Count - 2], numbers[numbers.Count - 1], numbers[0]);
                numbers.Remove(tuple.Item1);
                numbers.Remove(tuple.Item2);
                numbers.Remove(tuple.Item3);
                return tuple;
            }
            var tuple2 = (numbers[currentPos + 1], numbers[currentPos + 2], numbers[currentPos + 3]);
            numbers.Remove(tuple2.Item1);
            numbers.Remove(tuple2.Item2);
            numbers.Remove(tuple2.Item3);
            return tuple2;
        }
        private int FoundDestination(int currentNumber)
        {
            currentNumber--;
            while (!numbers.Contains(currentNumber))
            {
                currentNumber--;
                if (currentNumber <= 0)
                {
                    currentNumber = 9;
                }
            }
            return currentNumber;
        }
        private void Start(int addExtra,int moveMax)
        {
            if (addExtra != 0)
            {
                numbers.AddRange(Enumerable.Range(10, 1_000_001 - 10));
            }

            var pickUp = (0, 0, 0);
            var destination = 0;
            var currentNumber = numbers[0];
            var move = 1;
            while (move <= moveMax)
            {
                pickUp = PickUp(numbers.IndexOf(currentNumber));
                destination = FoundDestination(currentNumber);
                var position = numbers.IndexOf(currentNumber) + 1;
                if (position == numbers.Count)
                {
                    position = 0;
                }
                currentNumber = numbers[position];
                numbers.Insert(numbers.IndexOf(destination) + 1, pickUp.Item1);
                numbers.Insert(numbers.IndexOf(pickUp.Item1) + 1, pickUp.Item2);
                numbers.Insert(numbers.IndexOf(pickUp.Item2) + 1, pickUp.Item3);
                move++;
            }
        }
        public void Part1()
        {
            var initialState = numbers.ToList();
            Start(0,100);
            Console.WriteLine($"Day 22: Ticket Translation {string.Join(string.Empty,numbers)} (part1)");
            numbers = initialState;
        }
        public void Part2()
        {
            var initialState = numbers.ToList();
            Start(1, 10000000);
            Console.WriteLine($"Day 22: Ticket Translation {numbers[numbers.IndexOf(1)+1]} (part1)");
            Console.WriteLine($"Day 22: Ticket Translation {numbers[numbers.IndexOf(1) + 2]} (part1)");
            numbers = initialState;
        }
    }

}
