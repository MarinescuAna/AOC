using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day17 : BaseDay
    {
        private (int, int) x = (0, 0);
        private (int, int) y = (0, 0);

        public Day17() : base(17, "Trick Shot")
        {
            ReadInput();
        }


        private int ComputeHighestPoz()
        {
            var currentPoz = 0;

            for(var speed = (y.Item1 * -1) - 1; speed > 0; speed--)
            {
                currentPoz += speed;
            }
            return currentPoz;
        }
        public override void Part1()
        {
            Display(ComputeHighestPoz().ToString(), true);
        }

        public override void Part2()
        {
            
        }

        public override void ReadInput()
        {
            x = (20, 30); 
            y = (-10, -5);
            x = (175, 227);
            y = (-134, -79);
        }
    }
}
