using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day24Process
    {
        private static Day24 Day = new Day24();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }

    public class Day24
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In24.txt");
        //private List<Tile> tiles = new List<Tile>();
        public Day24()
        {
            ReadInput();
        }

       
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
              
            }
        }
       
        public void Part1()
        {
            var prod = 1L;
            
            Console.WriteLine("Day 24: Ticket Translation {0} (part1)", prod);
        }
          public void Part2()
        {
            
    
            //Console.WriteLine("Day 24: Ticket Translation {0} (part2)", prod);
        }
    }

}
