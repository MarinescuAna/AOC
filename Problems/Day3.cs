using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day3 : BaseDay
    {
        private List<string> input;
        public Day3() : base(3, "Binary Diagnostic")
        {
           
            input = new List<string>();
            ReadInput();
        }

        public override void Part1()
        {
            var bits = FindOneApparitions(input);
            (string mostCommon, string leastCommon) number = (string.Empty, string.Empty);

            foreach (var bite in bits)
            {
                if ((input.Count - bite) == bite)
                {
                    number.Item1 += '1';
                    number.Item2 += '0';
                }
                else if ((input.Count - bite) > bite)
                {
                    number.Item1 += '0';
                    number.Item2 += '1';
                }
                else
                {
                    number.Item1 += '1';
                    number.Item2 += '0';

                }
            }
            Display((ConvertToBinary(number.mostCommon) * ConvertToBinary(number.leastCommon)).ToString(), true);
        }

        private int ConvertToBinary(string number)
        {
            var power = input[0].Length-1;
            var convertedNumber = 0;
            foreach(var digit in number)
            {
                convertedNumber += (int)Math.Pow(2, power) * (digit == '0' ? 0 : 1);
                    power--;
            }
            return convertedNumber;
        }

        private string NumberLeft(bool oxygenGeneratorRating)
        {
            var numbersLeft = input.ToList();

            for (var i = 0; numbersLeft.Count > 1 && i <= input[0].Length; i++)
            {
                var oneBitsCount = numbersLeft.Where(u => u[i] == '1').ToList().Count;
                var mask = oxygenGeneratorRating?
                    (numbersLeft.Count - oneBitsCount) > oneBitsCount ? '0' : '1':
                    (numbersLeft.Count - oneBitsCount) > oneBitsCount ? '1' : '0';

                numbersLeft = numbersLeft.Where(u => u[i] == mask).ToList();
            }

            return numbersLeft[0];
        }


        public override void Part2()
        {
            var oxygenGeneratorRating = NumberLeft(true);
            var CO2ScrubberRating = NumberLeft(false);
            Display((ConvertToBinary(oxygenGeneratorRating) * ConvertToBinary(CO2ScrubberRating)).ToString(), false);
        }
        private int[] FindOneApparitions(List<string> numbers)
        {
            var bitsOne = new int[input[0].Length];
            for (var i = 0; i < input[0].Length; i++)
            {
                bitsOne[i] = numbers.Where(u => u[i] == '1').ToList().Count;
            }

            return bitsOne;
        }
        public override void ReadInput()
        {
            input = StreamReader.ReadToEnd().Split(Environment.NewLine).ToList();
        }
    }
}
