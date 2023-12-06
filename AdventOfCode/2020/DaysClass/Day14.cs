using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day14Process
    {
        private static Day14 Day14 = new Day14();
        public static void Process()
        {
            Day14.Part1();
            Day14.Part2();
        }
    }
    public class Day14
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In14.txt");
        private List<string> lines = new List<string>();
        public Day14()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }

        private void SplitMask(List<(int, int)> list, string mask, int i)
        {
            for (var index = 0; index < mask.Length; index++)
            {
                switch (i)
                {
                    case 1:
                        {
                            if (mask[index] != 'X')
                            {
                                list.Add((35 - index, int.Parse(mask[index].ToString())));
                            }
                            break;
                        }
                    case 2:
                        {
                            if (mask[index] != '0')
                            {
                                list.Add(((35 - index), mask[index] == 'X' ? 2 : int.Parse(mask[index].ToString())));
                            }

                            break;
                        }
                }
            }
        }

        private int[] ToIntArray(long number)
        {
            var index = 0;
            var bytes = new int[64];
            var temp = new int[64];
            while (number > 0)
            {
                temp[index++] = (int)number % 2;
                number /= 2;
            }

            for (var i = index - 1; i >= 0; i--)
            {
                bytes[i] = temp[i];
            }

            return bytes;
        }
        private long ToInt64(int[] bytes)
        {
            long number = 0;
            for (var i = 0; i < 64; i++)
            {
                number = (long)(number + (i == 0 && bytes[0] == 0 ? 0 : Math.Pow((long)bytes[i] * 2, i)));
            }

            return number;
        }
        private long Sum(Dictionary<long,long> memory)
        {
            long sum = 0;
            foreach(var address in memory)
            {
                sum += address.Value;
            }
            return sum;
        }
        public void Part1()
        {
            var unchagePositions = new List<(int, int)>();
            var memory = new Dictionary<long,long>();

            foreach (var line in lines)
            {
                if (line.Contains("mask"))
                {
                    unchagePositions.Clear();
                    SplitMask(unchagePositions, line.Replace("mask = ", ""), 1);
                }
                else
                {
                    if (line.Contains("mem"))
                    {
                        var split = line.Replace("mem[", "").Replace("] =", "").Split(" ");
                        var bytes = ToIntArray(Int64.Parse(split[1]));
                        foreach (var pair in unchagePositions)
                        {
                            bytes[pair.Item1] = (byte)pair.Item2;
                        }
                        var key = int.Parse(split[0]);
                        if (memory.ContainsKey(key))
                        {
                            memory[key] = ToInt64(bytes);
                        }
                        else
                        {
                            memory.Add(key, ToInt64(bytes));
                        }
                        
                    }
                }
            }
            Console.WriteLine("Day 14: Docking Data {0} (part1)", Sum(memory));

        }
        private int SearchForX(int[] address)
        {
            for(var bite=0;bite<64;bite++)
            {
                if (address[bite] == 2)
                {
                    return bite;
                } 
            }
            return -1;
        }
        private void GenerateAddresses(Dictionary<long,long> memory, int[] address, long value)
        {
            if (SearchForX(address)==-1)
            {
                long indexToChange = ToInt64(address);
                if (memory.ContainsKey(indexToChange))
                {
                    memory[indexToChange] = value;
                }
                else
                {
                    memory.Add(indexToChange, value);
                }
            }
            else
            {
                var address0 = ReplaceFirstMatch((int[])address.Clone(), 0);
                var address1 = ReplaceFirstMatch((int[])address.Clone(), 1);
                GenerateAddresses(memory, (int[])address0.Clone(), value);
                GenerateAddresses(memory, (int[])address1.Clone(), value);
            }
        }

        private int[] ReplaceFirstMatch(int[] address, int newValue)
        {
            int index = SearchForX(address);
            if (index < 0)
                return address;
            address[index] = newValue;
            return address;
        }
        public void Part2()
        {
            var unchagePositions = new List<(int, int)>();
            var memory = new Dictionary<long,long>();

            foreach (var line in lines)
            {
                if (line.Contains("mask"))
                {
                    unchagePositions.Clear();
                    SplitMask(unchagePositions, line.Replace("mask = ", ""), 2);
                }
                else
                {
                    if (line.Contains("mem"))
                    {
                        var split = line.Replace("mem[", "").Replace("] =", "").Split(" ");
                        var bytes = ToIntArray(Int64.Parse(split[0]));
                        foreach (var pair in unchagePositions)
                        { 
                            bytes[pair.Item1] = pair.Item2;
                        }
                        GenerateAddresses(memory, (int[])bytes.Clone(),int.Parse(split[1]));
                    }
                }
            }

            Console.WriteLine("Day 14: Docking Data {0} (part2)", Sum(memory));
        }

    }
}
