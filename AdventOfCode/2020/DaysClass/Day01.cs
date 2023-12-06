using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day01Process
    {
        private static Day01 Day = new Day01();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day01
    {
        private static StreamReader file = new StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In.txt");
        private string line;
        private int[] allNumbers = new int[1000];
        private int contor = 0;
        public Day01()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            while ((line = file.ReadLine()) != null)
            {
                allNumbers[contor++] = Int32.Parse(line);
            }
        }
        public void Part1()
        {
            for (int i = 0; i < contor; i++)
            {
                for (int j = i + 1; j < contor; j++)
                {
                    if (allNumbers[i] + allNumbers[j] == 2020)
                    {
                        Console.WriteLine("Day 1: Report Repair {0} + {1} = 2020 (part1)",allNumbers[i],allNumbers[j]);
                        Console.WriteLine("Day 1: Report Repair {0} + {1} = {2} (part1)", allNumbers[i], allNumbers[j], allNumbers[i] * allNumbers[j]);
                        break;
                    }
                }
            }
        }
        public void Part2()
        {
            for (int i = 0; i < contor; i++)
            {
                for (int j = i + 1; j < contor; j++)
                {
                    for (int k = j + 1; k < contor; k++)
                    {
                        if (allNumbers[i] + allNumbers[j] + allNumbers[k] == 2020)
                        {
                            Console.WriteLine("Day 1: Report Repair {0} + {1} + {2} = 2020 (part2)",allNumbers[i], allNumbers[j] ,allNumbers[k]);
                            Console.WriteLine("Day 1: Report Repair {0} + {1} + {2} = {3} (part2)",
                                allNumbers[i],
                                allNumbers[j],
                                allNumbers[k],
                                allNumbers[i] * allNumbers[j] * allNumbers[k]);
                            break;
                        }
                    }
                }
            }
        }
    }
}
