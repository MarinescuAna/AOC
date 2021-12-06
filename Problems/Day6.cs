using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day6 : BaseDay
    {
        public Day6() : base(6, "Lanternfish")
        {
            ReadInput();
        }
        private long ReproduceLanternfishs(int DaysPassing)
        {
            long[] LanternfishsTime = new long[9];
            foreach (var ch in line.Split(','))
            {
                LanternfishsTime[int.Parse(ch)]++;
            }

            for (var t = 0; t < DaysPassing; t++)
            {
                LanternfishsTime[(t + 7) % 9] += LanternfishsTime[t % 9];
            }
            var sum = (long)0;
            for (var n = 0; n < LanternfishsTime.Length; n++)
            {
                sum += LanternfishsTime[n];
            }
            return sum;
        }
        public override void Part1()
        {     
            Display(ReproduceLanternfishs(80).ToString(), true);
        }

        public override void Part2()
        {
            Display(ReproduceLanternfishs(256).ToString(), false);
        }

        public override void ReadInput()
        {
            line = StreamReader.ReadLine();
        }
    }
}
