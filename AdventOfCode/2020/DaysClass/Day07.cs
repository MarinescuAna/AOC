using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day07Process
    {
        private static Day07 Day = new Day07();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day07
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(Constants.Path+"In7.txt");
        private List<string> bagsContainShinyGold = new List<string>();
        private Dictionary<string, List<(int,string)>> allBags = new Dictionary<string, List<(int, string)>>();
        public Day07()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            var line = string.Empty;
            var temp_bags = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                Parse(line);
                temp_bags.Add(line);
            }
        }
        private void Parse(string line)
        {
            line = line.Replace(" bags", "").Replace(" bag", "").Replace(".", "");
            var split = line.Split(" contain ");

            if (split[1].Contains("shiny gold"))
            {
                bagsContainShinyGold.Add(split[0]);
            }

            var sum = 0;
            if (split[1].Contains("no other"))
            {
                allBags.Add(split[0], new List<(int, string)>());
            }
            else
            {
                if (split[1].Contains(","))
                {
                    var list = new List<(int, string)>();
                    foreach (var bag in split[1].Split(", "))
                    {
                        var number = int.Parse(bag[0].ToString());
                        var tag = bag.Replace(bag[0] + " ", "");
                        list.Add((number,tag));
                        sum += number;
                    }
                    allBags.Add(split[0], list);
                }
                else
                {
                    var number = int.Parse(split[1][0].ToString());
                    var tag = split[1].Replace(split[1][0] + " ", "");
                    allBags.Add(split[0], new List<(int, string)> { (number,tag) });
                }
            }

        }
        public void Part1()
        {
            Func<string, string, bool> searchForBag = (bag, bag2) =>
            {
                var result = allBags[bag].Find(u => u.Item2.Contains(bag2));
                if ( result != (0,null))
                {
                    bagsContainShinyGold.Add(bag);
                    return true;
                }
                return false;
            };

            var continu = true;
            while (continu)
            {
                continu = false;
                foreach (var bag in allBags)
                {
                    var duplicate = bagsContainShinyGold.Find(u=>u.Contains(bag.Key))!=null;

                    if (!duplicate)
                    {
                        foreach (var bag2 in bagsContainShinyGold)
                        {
                            continu = searchForBag(bag.Key, bag2);
                            if (continu)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Day 7: Handy Haversacks {bagsContainShinyGold.Count} (part1)");
        }

        private int Compute(string key)
        {
            if (allBags[key].Count == 0)
            {
                return 0;
            }

            var sum=0;
            foreach(var bag in allBags[key])
            {
                sum = sum + bag.Item1 + bag.Item1*Compute(bag.Item2);
            }
            return sum;

        }
        public void Part2()
        {
            Console.WriteLine($"Day 7: Handy Haversacks {Compute("shiny gold")} (part2)");
        }
    }
}
