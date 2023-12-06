using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC.DaysClass
{
    public class Day19
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In19.txt");
        private Dictionary<int, string> Rules = new Dictionary<int,  string>();
        private List<string> ReceivedMessage = new List<string>();
        public Day19()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            var line = string.Empty;
            while (!string.IsNullOrEmpty(line = file.ReadLine()))
            {
                line.Replace(" ", "");
                if (line.Contains(':'))
                {
                    Rules.Add(int.Parse(line.Split(':')[0]), line.Split(':')[1].Substring(1));
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    ReceivedMessage.Add(line);
                }
            }
        }

        private string BuildPattern()
        {
            string rule = Rules[0];
            Regex regex = new Regex(@"\d+", RegexOptions.Compiled);
            while (true)
            {
                Match match = regex.Match(rule);
                if (match.Success)
                {
                    string thing = Rules[int.Parse(match.Value)];
                    if (thing.Contains("\""))
                    {
                        thing = thing.Substring(1, thing.Length - 2);
                    }
                    else
                    {
                        thing = "(" + thing + ")";
                    }
                    rule = regex.Replace(rule, thing, 1);
                }
                else
                {
                    break;
                }
            }
            return rule.Replace(" ","");
        }
        public void Part1()
        {
            var pattern = BuildPattern();
            Console.WriteLine($"Day 19: Conway Cubes {ReceivedMessage.Where(s=> Regex.IsMatch(s, "^" + pattern + "$")).Count()} (part1)");
        }

        public void Part2()
        {
            var pattern = BuildPattern();
            var list = ReceivedMessage.Where(s => Regex.IsMatch(s, "^" + pattern + "$"));
            foreach(var t in list)
            {
                Console.WriteLine(t);
            }
           // Console.WriteLine($"Day 19: Conway Cubes {list.Count} (part1)");
        }
    }

    public static class Day19Process
    {
        private static Day19 Day19 = new Day19();
        public static void Process()
        {
            Day19.Part1();
            Day19.Part2();
        }
    }
}
