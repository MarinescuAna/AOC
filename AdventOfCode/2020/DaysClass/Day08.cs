using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day08Process
    {
        private static Day08 Day = new Day08();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day08
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In8.txt");
        private List<string> operations = new List<string>();
        private int[] timeExecution = new int[1000];
        public Day08()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                operations.Add(line);
            }
        }
        private void Restart()
        {
            for(var index = 0; index <= operations.Count; index++)
            {
                timeExecution[index] = 0;
            }
        }
        private bool SecondTimeLoop(out int acumulator)
        {
            Restart();
            acumulator = 0;
            for (var index = 0; index < operations.Count;)
            {
                var splitOperation = operations[index].Split(" ");
                var argument = int.Parse(splitOperation[1]); 

                if (timeExecution[index] == 1)
                {
                    return true;
                }
                timeExecution[index]++;

                switch (splitOperation[0])
                {
                    case "acc":
                        {
                            acumulator += argument;
                            index++;
                            break;
                        }
                    case "jmp":
                        {
                            index += argument;
                            break;
                        }
                    default:
                        {
                            index++;
                            break;
                        }
                }

            }

            return false;
        }

        public void Part1()
        {
            int acumulator = 0;
            SecondTimeLoop(out acumulator);
            Console.WriteLine("Day 8: Custom Customs {0} (part1)", acumulator);
        }
        public void Part2()
        {
            var acumulator = 0;

            for(var index = operations.Count-1; index >= 0; index--)
            {
                if (operations[index].Contains("jmp"))
                {
                    operations[index]=operations[index].Replace("jmp", "nop");
                    if(SecondTimeLoop( out acumulator))
                    {
                        operations[index] = operations[index].Replace("nop", "jmp");
                    }
                    else
                    {
                        break;
                    }
                }
                if (operations[index].Contains("nop"))
                {
                    operations[index]=operations[index].Replace("nop", "jmp");
                    if (SecondTimeLoop( out acumulator))
                    {
                        operations[index] = operations[index].Replace("jmp", "nop");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Day 8: Custom Customs {0} (part2)", acumulator);
        }
    }
}
