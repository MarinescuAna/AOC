using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day8 : BaseDay
    {
        private List<string[]> DigitsAfter;
        private List<string[]> DigitsBefore;
        public Day8() : base(8, "Seven Segment Search")
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
        private void InitNumbers(Dictionary<int, string> numbers)
        {
            numbers.Clear();
            for (var i = 0; i < 10; i++)
            {
                numbers.Add(i, "");
            }
        }

        private void OrderNumbers(Dictionary<int, string> pairs)
        {
            for(var i=0; i<10;i++)
            {
                pairs[i] = OrderDigits(pairs[i]);
            }
        }

        private string OrderDigits(string v)
        {
            return String.Concat(v.OrderBy(c => c));
        }

        private string SearchForNumer(Dictionary<int, string> pairs, string number)
        {
            foreach (var pair in pairs)
            {
                if (pair.Value.Equals(OrderDigits(number)))
                {
                    return pair.Key.ToString();
                }
            }

            return string.Empty;
        }
        public override void Part2()
        {
            var numbers = new Dictionary<int, string>();
            var number = string.Empty;
            var sum = (long)0;
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
                numbers[9] = DigitsBefore[i].First(u => u.Except(numbers[5]).Count() == 6 && !u.Equals(numbers[6]) && !u.Equals(numbers[0]));
                numbers[5] = DigitsBefore[i].First(u => u.Except(numbers[4]).Count() == 2 && !u.Equals(numbers[3]) && !u.Equals(numbers[9]));
                numbers[2] = DigitsBefore[i].First(u => u.Except(numbers[5]).Count() == 2 && !u.Equals(numbers[8]) && !u.Equals(numbers[0]));

                number = string.Empty;
                OrderNumbers(numbers);
                foreach (var nr in DigitsAfter[i])
                {
                    number+=SearchForNumer(numbers, nr);
                }
                sum += long.Parse(number); 
            }
            Display(sum.ToString(), false);
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
