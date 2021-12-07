using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day7 : BaseDay
    {
        private List<int> CrabsHorizontalPos;
        public Day7() : base(7, "The Treachery of Whales")
        {
            CrabsHorizontalPos = new List<int>();
            ReadInput();
        }
        private int ComputeFuelForValue(int valKey)
        {
            return CrabsHorizontalPos.Select(u=>Math.Abs(u-valKey)).Sum();
        }
        public override void Part1()
        {
            var min = 999999;
            foreach(var v in CrabsHorizontalPos)
            {
                var fuel = ComputeFuelForValue(v);
                if (fuel < min)
                {
                    min = fuel;
                }
            }
           
            Display(min.ToString(), true);
        }
        private int ComputeFuelForValue2(int poz)
        {
            return CrabsHorizontalPos.Select(u => Math.Abs(u - poz) * (Math.Abs(u - poz) + 1) / 2).Sum();
        }
        public override void Part2()
        {
            var min = ComputeFuelForValue2(0);
            for (var i = 1; i < CrabsHorizontalPos.Count; i++)
            {
                var fuel = ComputeFuelForValue2(i);
                if (fuel < min)
                {
                    min = fuel;
                }
            }

            Display(min.ToString(), false);
        }

        public override void ReadInput()
        {
            CrabsHorizontalPos = StreamReader.ReadLine().Split(",").Select(n=>int.Parse(n)).ToList();      
        }
    }
}
