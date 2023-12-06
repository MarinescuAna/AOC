using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day18Process
    {
        private static Day18 Day18 = new Day18();
        public static void Process()
        {
            Day18.Part1();
            Day18.Part2();
        }
    }
    public class Day18
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In18.txt");
        private List<string> Expressions = new List<string>();

        public Day18()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            var line = string.Empty;
            while (!string.IsNullOrEmpty(line = file.ReadLine()))
            {
                Expressions.Add(line.Replace(" ", ""));
            }
        }
        private (long, int) Parantheses(string line, int begin)
        {
            long acumulator = 0;
            var operation = ' ';
            while (begin < line.Length)
            {
                if ("0123456789".Contains(line[begin]))
                {
                    switch (operation)
                    {
                        case '+':
                            {
                                acumulator += long.Parse(line[begin].ToString());
                                break;
                            }
                        case '*':
                            {
                                acumulator = acumulator == 0 ? long.Parse(line[begin].ToString()) :
                            acumulator * long.Parse(line[begin].ToString());
                                break;
                            }
                        case ' ':
                            {
                                acumulator = long.Parse(line[begin].ToString());
                                break;
                            }
                    }
                }
                else
                {
                    if (line[begin] == '(')
                    {
                        var result = Parantheses(line, begin + 1);
                        switch (operation)
                        {
                            case '+':
                                {
                                    acumulator += result.Item1;
                                    break;
                                }
                            case '*':
                                {
                                    acumulator = acumulator == 0 ? result.Item1 : acumulator * result.Item1;
                                    break;
                                }
                            case ' ':
                                {
                                    acumulator = result.Item1;
                                    break;
                                }
                        }
                        begin = result.Item2 - 1;
                    }
                    else
                    if (line[begin] == ')')
                    {
                        return (acumulator, begin + 1);
                    }
                    else
                    {
                        operation = line[begin];
                    }

                }
                begin++;
            }
            return (0, 0);
        }
        public void Part1()
        {
            var acumulator = 0L;
            var operation = ' ';
            var results = new List<long>();
            foreach (var line in Expressions)
            {
                acumulator = 0;
                operation = ' ';
                for (var i = 0; i < line.Length; i++)
                {
                    if ("0123456789".Contains(line[i]))
                    {
                        switch (operation)
                        {
                            case '+':
                                {
                                    acumulator += long.Parse(line[i].ToString());
                                    break;
                                }
                            case '*':
                                {
                                    acumulator = acumulator == 0 ? long.Parse(line[i].ToString()) : acumulator * long.Parse(line[i].ToString());
                                    break;
                                }
                            case ' ':
                                {
                                    acumulator = long.Parse(line[i].ToString());
                                    break;
                                }
                        }
                    }
                    else
                    {
                        if (line[i] == '(')
                        {
                            var result = Parantheses(line, i + 1);
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += result.Item1;
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? result.Item1 : acumulator * result.Item1;
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = result.Item1;
                                        break;
                                    }
                            }
                            i = result.Item2 - 1;
                        }
                        else
                        {
                            operation = line[i];
                        }
                    }
                }
                results.Add(acumulator);
            }

            Console.WriteLine($"Day 18: Operation Order {results.Sum()} (part1)");
        }
        private (long, int) Parantheses2(string line, int begin)
        {
            long acumulator = 0;
            var operation = ' ';
            while (begin < line.Length)
            {
                if (line[begin] == '(' && AdditionIsNext(line, begin))
                {
                    var result = Addition(line, begin);
                    switch (operation)
                    {
                        case '+':
                            {
                                acumulator += result.Item1;
                                break;
                            }
                        case '*':
                            {
                                acumulator = acumulator == 0 ? result.Item1 :
                            acumulator * result.Item1;
                                break;
                            }
                        case ' ':
                            {
                                acumulator = result.Item1;
                                break;
                            }
                    }
                    begin = result.Item2 - 1;
                }
                else
                {
                    if ("0123456789".Contains(line[begin]))
                    {
                        if (line[begin + 1] == '+')
                        {
                            var result = Addition(line, begin);
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += result.Item1;
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? result.Item1 :
                                    acumulator * result.Item1;
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = result.Item1;
                                        break;
                                    }
                            }
                            begin = result.Item2 - 1;
                        }
                        else
                        {
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += long.Parse(line[begin].ToString());
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? long.Parse(line[begin].ToString()) :
                                    acumulator * long.Parse(line[begin].ToString());
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = long.Parse(line[begin].ToString());
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        if (line[begin] == '(')
                        {
                            var result = Parantheses2(line, begin + 1);
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += result.Item1;
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? result.Item1 : acumulator * result.Item1;
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = result.Item1;
                                        break;
                                    }
                            }
                            begin = result.Item2;
                        }
                        else
                        if (line[begin] == ')')
                        {
                            return (acumulator, begin);
                        }
                        else
                        {
                            operation = line[begin];
                        }

                    }
                }
                begin++;
            }
            return (0, 0);
        }
        private (long, int) Addition(string line, int begin)
        {
            var acumulator = 0L;
            while (begin < line.Length)
            {

                if ("0123456789".Contains(line[begin]))
                {
                    acumulator += long.Parse(line[begin].ToString());
                }
                else
                {
                    if (line[begin] == '(')
                    {
                        var result = Parantheses2(line, begin + 1);
                        acumulator += result.Item1;
                        begin = result.Item2;
                    }
                }

                if (begin == line.Length - 1 || (line[begin] == ')' && line[begin + 1] != '+') || (line[begin + 1] != '+' && "0123456789".Contains(line[begin]) && line[begin + 1] != '('))
                {
                    return (acumulator, begin + 1);
                }

                begin++;
            }
            return (0, 0);
        }
        private bool AdditionIsNext(string line, int index)
        {
            var param = 0;
            while (index < line.Length && line[index] != ')')
            {
                if(line[index] == '(')
                {
                    param++;
                }
                index++;

                if (line[index] == ')')
                {
                    param--;
                    if (param != 0)
                    {
                        index++;
                    }
                }
                
            }
            return index == line.Length - 1 ? false : line[index + 1] == '+';
        }
        public void Part2()
        {
            var acumulator = 0L;
            var operation = ' ';
            var results = new List<long>();
            foreach (var line in Expressions)
            {
                acumulator = 0;
                operation = ' ';
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '(' && AdditionIsNext(line, i))
                    {
                        var result = Addition(line, i);
                        switch (operation)
                        {
                            case '+':
                                {
                                    acumulator += result.Item1;
                                    break;
                                }
                            case '*':
                                {
                                    acumulator = acumulator == 0 ? result.Item1 :
                                acumulator * result.Item1;
                                    break;
                                }
                            case ' ':
                                {
                                    acumulator = result.Item1;
                                    break;
                                }
                        }
                        i = result.Item2;
                    }
                    if (i >= line.Length)
                    {
                        continue;
                    }
                    if ("0123456789".Contains(line[i]))
                    {
                        if (i != line.Length - 1 && line[i + 1] == '+')
                        {
                            var result = Addition(line, i);
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += result.Item1;
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? result.Item1 :
                                    acumulator * result.Item1;
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = result.Item1;
                                        break;
                                    }
                            }
                            i = result.Item2 - 1;
                        }
                        else
                        {
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += long.Parse(line[i].ToString());
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? long.Parse(line[i].ToString()) : acumulator * long.Parse(line[i].ToString());
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = long.Parse(line[i].ToString());
                                        break;
                                    }
                            }
                        }
                    }
                    else
                    {
                        if (line[i] == '(')
                        {
                            var result = Parantheses2(line, i + 1);
                            switch (operation)
                            {
                                case '+':
                                    {
                                        acumulator += result.Item1;
                                        break;
                                    }
                                case '*':
                                    {
                                        acumulator = acumulator == 0 ? result.Item1 : acumulator * result.Item1;
                                        break;
                                    }
                                case ' ':
                                    {
                                        acumulator = result.Item1;
                                        break;
                                    }
                            }
                            i = result.Item2;
                        }
                        else
                        {
                            operation = line[i];
                        }
                    }
                }
                results.Add(acumulator);
            }

            Console.WriteLine($"Day 18: Operation Order {results.Sum()} (part2)");
        }
    }
}
