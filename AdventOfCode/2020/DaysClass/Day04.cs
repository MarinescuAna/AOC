using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC.DaysClass
{
    public static class Day04Process
    {
        private static Day04 Day = new Day04();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day04
    {
        private readonly string[] fields = {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
            };

        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In4.txt");
        private List<string> passports;
        private List<string> valid_passports = new List<string>();
        public Day04()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            var temp_passaport = new StringBuilder();
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    temp_passaport.Append("\n");
                }
                else
                {
                    temp_passaport.Append(" " + line);
                }
            }
            passports = new List<String>(temp_passaport.ToString().Split("\n"));
        }
        public void Part1()
        {

            foreach (string passport in passports)
            {
                bool validB = true;
                foreach (var item in fields)
                {
                    if (!passport.Contains(item)) validB = false;
                }

                if (validB)
                {
                    valid_passports.Add(passport);
                }
            }

            Console.WriteLine("Day 4: Passport Processing  {0} (part1)", valid_passports.Count);
        }
        private bool validByrIyrEyr(int number, int min, int max)
        {
            return number >= min && number <= max;
        }
        private bool validHgt(string type, int number)
        {
            return type == "in" ?
                number >= 59 && number <= 76 :
                number >= 150 && number <= 193;
        }
        public void Part2()
        {
            var trueValidPassaports = 0;

            foreach (var passport in valid_passports)
            {
                var valid = true;
                var information = passport.Split(" ");

                foreach (var item in information)
                {
                    if (string.IsNullOrEmpty(item))
                    {
                        continue;
                    }

                    var tag = item.Split(":")[0];
                    var utilInfo = item.Split(":")[1];

                    switch (tag)
                    {
                        case "byr":
                            {
                                valid = validByrIyrEyr(Int32.Parse(utilInfo), 1920, 2002);
                                break;
                            }
                        case "iyr":
                            {
                                valid = validByrIyrEyr(Int32.Parse(utilInfo), 2010, 2020);
                                break;
                            }
                        case "eyr":
                            {
                                valid = validByrIyrEyr(Int32.Parse(utilInfo), 2020, 2030);
                                break;
                            }
                        case "hgt":
                            {
                                var type = utilInfo.Contains("in") ? "in" : "cm";
                                valid = validHgt(type,Int32.Parse(utilInfo.Replace(type, "")));
                                break;
                            }
                        case "hcl":
                            {
                                valid = Regex.IsMatch(utilInfo, "^#[0-9a-f]{6}$");
                                break;
                            }
                        case "ecl":
                            {
                                valid = "amb blu brn gry grn hzl oth".Contains(utilInfo) && utilInfo.Length==3;
                                break;
                            }
                        case "pid":
                            {
                                valid = utilInfo.Length == 9;
                                valid = valid?int.TryParse(utilInfo, out int l):false;
                                break;
                            }
                    }

                    if (valid == false) {
                        break;
                    }
                }
                if (valid)
                {
                    trueValidPassaports++;
                }
            }

            Console.WriteLine("Day 4: Passport Processing  {0} (part2)", trueValidPassaports-1);
        }
    }
}
