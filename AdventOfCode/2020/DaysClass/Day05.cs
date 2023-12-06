using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day05Process
    {
        private static Day05 Day = new Day05();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day05
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In5.txt");
        private List<string> codes = new List<string>();
        private int[] frequency = new int[900];
        public Day05()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                codes.Add(line);
            }
        }
        public void Part1()
        {
            var maxId = 0;
            var idTemp = 0;
            var limitMinRow = 0;
            var limitMaxRow = 127;
            var limitMaxCol = 7;
            var limitMinCol = 0;
            var numbersBetween = 0;
            var half = 0;
            foreach(var code in codes)
            {
                limitMinRow = 0;
                limitMaxRow = 127;
                limitMaxCol = 7;
                limitMinCol = 0;

                foreach (var letter in code)
                {
                    if (letter == 'F')
                    {
                        numbersBetween = limitMaxRow - limitMinRow;
                        half = numbersBetween==1? 0: (numbersBetween + 1) / 2;
                        if (half == 0)
                        {
                            idTemp = limitMinRow * 8;
                        }
                        else
                        { 
                            limitMaxRow = limitMinRow + half - 1;
                        }
                    }else if (letter == 'B')
                    {
                        numbersBetween = limitMaxRow - limitMinRow;
                        half = numbersBetween==1? 0: (numbersBetween + 1) / 2;
                        if (half == 0)
                        {
                            idTemp = limitMaxRow * 8;
                        }
                        else
                        {
                            limitMinRow += half;
                        }
                        
                    }else if (letter == 'R')
                    {
                        numbersBetween = limitMaxCol - limitMinCol;
                        half = numbersBetween == 1 ? 0 : (numbersBetween + 1) / 2;
                        if (half == 0)
                        {
                            idTemp += limitMaxCol;
                        }
                        else
                        {
                            limitMinCol += half;
                        }
                    }
                    else if (letter == 'L')
                    {
                        numbersBetween = limitMaxCol - limitMinCol;
                        half = numbersBetween == 1 ? 0 : (numbersBetween + 1) / 2;
                        if (half == 0)
                        {
                            idTemp += limitMinCol;
                        }
                        else
                        {
                            limitMaxCol = limitMinCol + half - 1;
                        }
                    }
                }

                frequency[idTemp] = 9;
                maxId = maxId < idTemp ? idTemp : maxId;
            }

            Console.WriteLine("Day 5: Binary Boarding {0}", maxId);
        }
        public void Part2()
        {
            for(var index=49; index < 807; index++)
            {
                if (frequency[index] != 9)
                {
                    Console.WriteLine("Day 5: Binary Boarding {0}", index);
                }
            }
        }
    }
}
