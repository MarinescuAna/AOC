using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day10 : BaseDay
    {
        private List<string> input;
        private List<string> unclosedChunks;
        public Day10() : base(10, "Syntax Scoring")
        {
            input = new List<string>();
            unclosedChunks = new List<string>();
            ReadInput();
        }
        private void InitChunks(Dictionary<char, int> chunks)
        {
            chunks.Add(')', 0);
            chunks.Add(']', 0);
            chunks.Add('}', 0);
            chunks.Add('>', 0);
        }
        private (char chunk, bool isCorrupted) CorruptedLinesMissChar(string line)
        {
            var chunks = new Stack<char>();
            foreach (var chunk in line)
            {
                switch (chunk)
                {
                    case ')':
                        {
                            var chunkFound = chunks.Pop();
                            if (chunkFound != '(')
                            {
                                return (chunk, true);
                            }
                            break;
                        }
                    case ']':
                        {
                            var chunkFound = chunks.Pop();
                            if (chunkFound != '[')
                            {
                                return (chunk, true);
                            }
                            break;
                        }
                    case '}':
                        {
                            var chunkFound = chunks.Pop();
                            if (chunkFound != '{')
                            {
                                return (chunk, true);
                            }
                            break;
                        }
                    case '>':
                        {
                            var chunkFound = chunks.Pop();
                            if (chunkFound != '<')
                            {
                                return (chunk, true);
                            }
                            break;
                        }
                    default:
                        chunks.Push(chunk);
                        break;
                }
            }
            if (chunks.Count != 0)
            {
                var unclosed = string.Empty;
                while (chunks.Count != 0)
                {
                    switch (chunks.Pop())
                    {
                        case '(':
                            unclosed += ')';
                            break;
                        case '[':
                            unclosed += ']';
                            break;
                        case '{':
                            unclosed += '}';
                            break;
                        case '<':
                            unclosed += '>';
                            break;
                    }
                }
                unclosedChunks.Add(unclosed);
            }

            return (' ', false);
        }

        public override void Part1()
        {
            Func<int, int> returnPoints = (u) =>
            {
                switch (u)
                {
                    case ')':
                        return 3;
                    case ']':
                        return 57;
                    case '}':
                        return 1197;
                    default:
                        return 25137;
                }
            };

            var chunks = new Dictionary<char, int>();

            InitChunks(chunks);

            foreach (var line in input)
            {
                var result = CorruptedLinesMissChar(line);
                if (result.isCorrupted)
                {
                    chunks[result.chunk]++;
                }
            }

            Display(chunks.Select(u => returnPoints(u.Key) * u.Value).Sum().ToString(), true);
        }

        public override void Part2()
        {
            var points = new List<long>();
            var sum = 0l;
            Func<int, int> returnPoints = (u) =>
            {
                switch (u)
                {
                    case ')':
                        return 1;
                    case ']':
                        return 2;
                    case '}':
                        return 3;
                    default:
                        return 4;
                }
            };
            unclosedChunks.Reverse();
            foreach (var line in unclosedChunks)
            {
                sum = 0l;
                foreach (var chunk in line)
                {
                    sum = sum * 5 + returnPoints(chunk);
                }
                points.Add(sum);
            }
            points.Sort();
            Display(points[(points.Count - 1) / 2].ToString(), false);
        }

        public override void ReadInput()
        {
            while ((line = StreamReader.ReadLine()) != null)
            {
                input.Add(line);
            }
        }
    }
}
