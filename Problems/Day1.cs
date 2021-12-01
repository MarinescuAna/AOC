using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day1 : BaseDay
    {
        private List<int> input = new List<int>();
        public Day1() : base(1, "Sonar Sweep")
        {
            ReadInput();
        }
        private int CountIncreses(List<int> numbers)
        {
            var increases = 0;
            for (var i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] - numbers[i - 1] > 0)
                {
                    increases++;
                }
            }
            return increases++;
        }
        public override void Part1()
        {
            Display(CountIncreses(input).ToString(), true);
        }

        public override void Part2()
        {
            var sum = new List<int>();

            for (var i=2;i<input.Count;i++)
            {
                sum.Add(input[i-2] + input[i-1] + input[i]);
            }

            Display(CountIncreses(sum).ToString(), false);
        }

        public override void ReadInput()
        {
            while ((line = StreamReader.ReadLine()) != null)
            {
                input.Add(int.Parse(line));
            }
        }
    }
}
