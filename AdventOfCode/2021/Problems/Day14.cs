using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day14 : BaseDay
    {
        private string PolymerTemplate;
        private Dictionary<string, char> PairInsertionRules = new Dictionary<string, char>();
        public Day14() : base(14, "")
        {
            ReadInput();
        }
        private string[] SplitInTwo(string str)
        {
            return Enumerable.Range(0, str.Length - 1).Select((x => str[x..(x + 2)])).ToArray();
        }
        private string ApplySteps(int noSteps)
        {
            var LettersTimes = new Dictionary<char, long>();
            var PairsTimes = new Dictionary<string, long>();
            var pairs = new Dictionary<string, long>();
            var result = SplitInTwo(PolymerTemplate);
            foreach (var pair in result)
            {
                if (pairs.ContainsKey(pair))
                {
                    pairs[pair]++;
                }
                else
                {
                    pairs.Add(pair, 1);
                }
            }
            LettersTimes = PolymerTemplate.Distinct().ToDictionary(u => u, u => (long)PolymerTemplate.Where(t => t == u).Count());

            for (var step = 0; step < noSteps; step++)
            {
                var newPairs = new Dictionary<string, long>();
                foreach (var pair in pairs)
                {
                    var concatResult = SplitInTwo(pair.Key.Insert(1, PairInsertionRules[pair.Key].ToString()));
                    if (LettersTimes.ContainsKey(PairInsertionRules[pair.Key]))
                    {
                        LettersTimes[PairInsertionRules[pair.Key]] += pair.Value;
                    }
                    else
                    {
                        LettersTimes.Add(PairInsertionRules[pair.Key], 1);
                    }
                    if (newPairs.ContainsKey(concatResult[0]))
                    {
                        newPairs[concatResult[0]] += pair.Value;
                    }
                    else
                    {
                        newPairs.Add(concatResult[0], pair.Value);
                    }
                    if (newPairs.ContainsKey(concatResult[1]))
                    {
                        newPairs[concatResult[1]] += pair.Value;
                    }
                    else
                    {
                        newPairs.Add(concatResult[1], pair.Value);
                    }
                }
                pairs = new Dictionary<string, long>(newPairs);
            }
            var min = LettersTimes.Min(u => u.Value);
            var max = LettersTimes.Max(u => u.Value);
            return (max - min).ToString();
        }
        public override void Part1()
        {
            Display(ApplySteps(10), true);
        }

        public override void Part2()
        {
            Display(ApplySteps(40), false);
        }

        public override void ReadInput()
        {
            PolymerTemplate = StreamReader.ReadLine();
            PairInsertionRules = StreamReader.ReadToEnd().Split(Environment.NewLine).ToDictionary(u => u.Split(" -> ")[0], u => char.Parse(u.Split(" -> ")[1]));
        }
    }
}
