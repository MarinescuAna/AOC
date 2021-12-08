using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day8   : BaseDay
    {
        private List<string[]> DigitsAfter;
        private List<string[]> DigitsBefore;
        public Day8() : base(8, "")
        {
            DigitsAfter = new List<string[]>();
            DigitsBefore = new List<string[]>();
            ReadInput();
        }

        public override void Part1()
        {
            var count = DigitsAfter.Select(u => u.Where(u => u.Length == 2 || u.Length == 3 || u.Length == 4 || u.Length == 7).Count()).Sum();
            Display(count.ToString(), true);
        }
        private void InitNumbers(Dictionary<int,string> numbers)
        {
            numbers.Clear();
            for(var i = 0; i < 10; i++)
            {
                numbers.Add(i, "");
            }
        }
  
        public override void Part2()
        {
            var numbers = new Dictionary<int, string>();
            for (var i = 0; i < DigitsAfter.Count(); i++)
            {
                InitNumbers(numbers);
                numbers[1] = DigitsBefore[i].First(u => u.Length == 2);
                numbers[4] = DigitsBefore[i].First(u => u.Length == 4);
                numbers[3] = DigitsBefore[i].First(u => u.Except(numbers[1]).Count() == 3);
                numbers[7] = DigitsBefore[i].First(u => u.Length == 3);
                numbers[8] = DigitsBefore[i].First(u => u.Length == 7);
                numbers[6] = DigitsBefore[i].First(u => u.Except(numbers[1]).Count() == 5 && !u.Equals(numbers[8]));
                numbers[0] = DigitsBefore[i].First(u => u.Except(numbers[3]).Count() == 2 && !u.Equals(numbers[8]) && !u.Equals(numbers[6]));
                
                var f = DigitsBefore[i].Select(u => u.Except(numbers[1]).Count()).ToArray();
            }
        }

        public override void ReadInput()
        {
            while ((line = StreamReader.ReadLine()) != null)
            {
                DigitsBefore.Add(line.Split(" | ")[0].Split(" "));
                DigitsAfter.Add(line.Split(" | ")[1].Split(" "));
            }
        }
    }
}
