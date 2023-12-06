using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day02Process
    {
        private static Day02 Day = new Day02();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day02
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In2.txt");
        private List<string> passwords=new List<string>();
        public Day02()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                passwords.Add(line);
            }
        }
        public void Part1()
        {
            int valid_part = 0;
            foreach (var line in passwords)
            {
                var split_line = line.Split(' ');
                var times_repeat = split_line[0].Split('-');
                var letter_repeat = split_line[1];
                var text = split_line[2];
                var contor = 0;

                foreach (var letter in text)
                {
                    if (letter == letter_repeat[0])
                    {
                        contor++;
                    }
                }

                if (contor >= Int32.Parse(times_repeat[0]) && contor <= Int32.Parse(times_repeat[1]))
                {
                    valid_part++;
                }

            }

            Console.WriteLine("Day 2: Password Philosophy {0} (part1)", valid_part);
        }
        public void Part2()
        {
            int valid_part2 = 0;
            foreach (var line in passwords)
            {
                var split_line = line.Split(' ');
                var times_repeat = split_line[0].Split('-');
                var letter_repeat = split_line[1];
                var text = split_line[2];

                if (Int32.Parse(times_repeat[1]) > text.Length && text[Int32.Parse(times_repeat[0]) - 1] == letter_repeat[0])
                {
                    valid_part2++;
                }
                else if ((text[Int32.Parse(times_repeat[0]) - 1] == letter_repeat[0]
                   && text[Int32.Parse(times_repeat[1]) - 1] != letter_repeat[0]) ||
                   (text[Int32.Parse(times_repeat[0]) - 1] != letter_repeat[0]
                   && text[Int32.Parse(times_repeat[1]) - 1] == letter_repeat[0]))
                {
                    valid_part2++;
                }

            }

            Console.WriteLine("Day 2: Password Philosophy {0} (part2)", valid_part2);
        }
    }
}
