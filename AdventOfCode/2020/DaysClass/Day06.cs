using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day06Process
    {
        private static Day06 Day = new Day06();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day06
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In6.txt");
        private List<string> groups = new List<string>();
        private int[] frequency = new int[200];
        private List<int> personsInEachGroup = new List<int>();
        public Day06()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            var temp = new StringBuilder();
            var persons = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    groups.Add(temp.ToString());
                    personsInEachGroup.Add(persons);
                    temp.Clear();
                    persons = 0;
                }
                else
                {
                    temp.Append(line);
                    persons++;
                }
            }
            groups.Add(temp.ToString());
            personsInEachGroup.Add(persons);
        }

        private int CountQuestions(string group, int limit)
        {
            var questions = 0;

            foreach (var question in group)
            {
                frequency[question]++;
            }
            for (var index = (int)'a'; index <= (int)'z'; index++)
            {
                if (frequency[index] >= limit)
                {
                    questions++;       
                }
                frequency[index] = 0;
            }
            return questions;
        }
        public void Part1()
        {
            var questions = 0;
            foreach (var group in groups)
            {
                questions += CountQuestions(group, 1);
            }
            Console.WriteLine("Day 6: Custom Customs {0} (part1)", questions);
        }
        public void Part2()
        {
            var questions = 0;
            for(var index=0; index<groups.Count;index++)
            {
                questions += CountQuestions(groups[index], personsInEachGroup[index]);
            }
            Console.WriteLine("Day 6: Custom Customs {0} (part2)", questions);
        }
    }
}
