using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2021
{
    public abstract class BaseDay : IBaseDay
    {
        protected StreamReader StreamReader;
        private string title;
        private int day;
        protected string line = string.Empty;
        public BaseDay(int day, string title)
        {
            this.title = title;
            this.day = day;
            StreamReader = new StreamReader(Path.GetFullPath(@$"Input\Day{day}.txt").Replace(@"\bin\Debug\netcoreapp3.1\", @"\"));
        }
        protected void Display(string result, bool first)
        {
            Console.WriteLine(first ?
                $"Part1 Day{day} ({title}): {result}" :
                $"Part2 Day{day} ({title}): {result}"
                );
        }
        public abstract void Part1();
        public abstract void Part2();
        public void CallParts(int callPart = 3)
        {
            if (callPart == 1)
            {
                Part1();
            }
            else if (callPart == 2)
            {
                Part2();
            }
            else
            {
                Part1();
                Part2();
            }

        }
        public abstract void ReadInput();
    }
}
