using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day2 : BaseDay
    {
        private List<(string instruction, int number)> input;
        public Day2() : base(2, "Drive!")
        {
            input = new List<(string instruction, int number)>();
            ReadInput();
        }

        public override void Part1()
        {
            var horizontal = 0;
            var depth = 0;
            foreach(var step in input)
            {
                switch (step.instruction)
                {
                    case "forward":
                        horizontal += step.number;
                        break;
                    case "down":
                        depth += step.number;
                        break;
                    case "up":
                        depth -= step.number;
                        break;
                }
            }

            Display((horizontal * depth).ToString(), true);
        }

        public override void Part2()
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var step in input)
            {
                switch (step.instruction)
                {
                    case "forward":
                        horizontal += step.number;
                        depth += (step.number*aim);
                        break;
                    case "down":
                        aim += step.number;
                        break;
                    case "up":
                        aim -= step.number;
                        break;
                }
            }
            Display((horizontal * depth).ToString(), false);
        }

        public override void ReadInput()
        {

            while ((line = StreamReader.ReadLine()) != null)
            {
                var splitVal = line.Split(" ");
                input.Add(( splitVal[0], int.Parse(splitVal[1])));
            }
        }
    }
}
