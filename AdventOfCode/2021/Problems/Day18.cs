using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Problems
{
    public class Day18 : BaseDay
    {
        private List<string> lines = new List<string>();
        public Day18() : base(18, "Snailfish ")
        {
            ReadInput();
        }
        private bool ShouldBeReduce(string pairs)
        {
            var sum = 0;
            foreach (var character in pairs)
            {
                if (character == '[')
                {
                    sum++;
                }
                else
                {
                    if (character == ']')
                    {
                        sum--;
                    }
                }

                if (sum == 5)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsGraterThen10(string pairs)
        {
            return pairs.Replace("[", "").Replace("]", "").Split(",").Select(u => int.Parse(u)).Where(u => u >= 10).Count() > 0;
        }
        private string Magnitude(StringBuilder expression)
        {
            var result = (false, 0);
            for(var i = 0;expression.ToString().Contains(","); i++)
            {
                while (i<expression.Length-1 && expression[i + 1] == '[') i++;
                if (i > expression.Length)
                {
                    i = -1;
                    continue;
                }
                if ((result = NextPair(expression.ToString(), i)).Item1)
                {
                    var pair = ReturnPair(expression.ToString(), i);
                    var newString = $"{pair.left*3+pair.right*2}";
                    expression = expression.Remove(i, result.Item2);
                    expression.Insert(i, newString);
                }
            }

            return expression.ToString();
        }
        private (bool,int) NextPair(string pairs, int startIndex)
        {
            var newString = string.Empty;
            for (var i = startIndex; i<pairs.Length; i++)
            {
                newString = string.Concat(newString, pairs[i].ToString());
                if (pairs[i] == ']')
                {
                    break;
                }
            }
            return (Regex.IsMatch(newString, @"^\[{1}[0-9]+,[0-9]+\]{1}$"),newString.Length);
        }
        private (int left, int right) ReturnPair(string pairs,int startIndex)
        {
            var newString = string.Empty;
            for (var i = startIndex + 1; pairs[i] != ']'; i++)
            {
                newString = string.Concat(newString, pairs[i].ToString()).Replace("[", "").Replace("]", "");
            }
            return (int.Parse(newString.Split(",")[0]), int.Parse(newString.Split(",")[1]));
        }
        private string AddToLeftOrRight(StringBuilder pair,int startIndex, int valueToAdd, bool toLeft)
        {
            if (toLeft)
            {
                for (; startIndex > -1 && !char.IsDigit(pair[startIndex]); startIndex--) ;

                if (startIndex > 0 && char.IsDigit(pair[startIndex]))
                {
                    var no = char.IsDigit(pair[startIndex - 1]) ? string.Concat(pair[startIndex-1], pair[startIndex ]) : pair[startIndex].ToString();
                    var number = int.Parse(no) + valueToAdd;
                    pair = pair.Remove(no.Length>1 ? startIndex-1:startIndex, no.Length);
                    pair = pair.Insert(no.Length>1 ? startIndex-1 : startIndex, number.ToString());
                }
            }
            else
            {
                for (; startIndex < pair.Length && !char.IsDigit(pair[startIndex]); startIndex++) ;

                if (startIndex < pair.Length-1 && char.IsDigit(pair[startIndex]))
                {
                    var no = char.IsDigit(pair[startIndex + 1]) ? string.Concat(pair[startIndex], pair[startIndex + 1]) : pair[startIndex].ToString();
                    var number = int.Parse(no) + valueToAdd;
                    pair = pair.Remove(startIndex, no.Length);
                    pair = pair.Insert(startIndex, number.ToString());
                }
            }

            return pair.ToString();
        }
        private string ReplaceWith0(StringBuilder pairs,ref int startIndex, int length)
        {
            for (; !char.IsDigit(pairs[startIndex]) && startIndex<pairs.Length; startIndex++) ;
            startIndex--;
            pairs = pairs.Remove(startIndex , length);
            pairs.Insert(startIndex , '0');
            return pairs.ToString();
        }
        private string Reduction(string pairs)
        {
            var countSquareBrackets = 0;
            for (var i = 0; i < pairs.Length; i++)
            {
                if (pairs[i] == '[')
                {
                    countSquareBrackets++;
                }
                else
                {
                    if (pairs[i] == ']')
                    {
                        countSquareBrackets--;
                    }
                }

                if (countSquareBrackets == 5)
                {
                    var result = NextPair(pairs, i);
                    if (result.Item1)
                    {
                        var pairToAdd = ReturnPair(pairs, i);
                        pairs = AddToLeftOrRight(new StringBuilder(pairs), i, pairToAdd.left, true);
                        pairs = AddToLeftOrRight(new StringBuilder(pairs), i + result.Item2, pairToAdd.right, false);
                        pairs = ReplaceWith0(new StringBuilder(pairs), ref i,result.Item2);
                        countSquareBrackets--;
                    }
                }
            }
            return pairs;
        }
        private string SplitString(StringBuilder pairs)
        {
            for (var i = 0; i < pairs.Length - 1; i++)
            {
                if (char.IsDigit(pairs[i]) && char.IsDigit(pairs[i + 1]))
                {
                    var numberToSplit = int.Parse(string.Concat(pairs[i].ToString(), pairs[i + 1].ToString()));
                    var newString = $"[{Math.Floor(numberToSplit / 2.0)},{Math.Ceiling(numberToSplit / 2.0)}]";
                    pairs = pairs.Remove(i, 2);
                    pairs = pairs.Insert(i, newString);
                    return pairs.ToString();
                }
            }

            return pairs.ToString();
        }
        public override void Part1()
        {
            string result = lines[0];
            var ReduceOrSplit = (false, false);
            for (var i = 1; i < lines.Count(); i++)
            {
                result = $"[{result},{lines[i]}]";
                while (ReduceOrSplit.Item1 = ShouldBeReduce(result) || (ReduceOrSplit.Item2= IsGraterThen10(result)))
                {
                    if (ReduceOrSplit.Item1)
                    {
                        result = Reduction(result);
                    }

                    if (IsGraterThen10(result))
                    {
                        result = SplitString(new StringBuilder(result));
                    }
                }
            }
            Display(Magnitude(new StringBuilder(result)), true);
        }

        public override void Part2()
        {
            string result = string.Empty;
            var maxMagnitude = "0";
            var ReduceOrSplit = (false, false);
            for (var i = 0; i < lines.Count(); i++)
            {
                for (var j = 0; j < lines.Count(); j++)
                {
                    result = $"[{lines[i]},{lines[j]}]";
                    while (ReduceOrSplit.Item1 = ShouldBeReduce(result) || (ReduceOrSplit.Item2 = IsGraterThen10(result)))
                    {
                        if (ReduceOrSplit.Item1)
                        {
                            result = Reduction(result);
                        }

                        if (IsGraterThen10(result))
                        {
                            result = SplitString(new StringBuilder(result));
                        }
                    }
                    var magnitude = Magnitude(new StringBuilder(result));
                    if (long.Parse(maxMagnitude) < long.Parse(magnitude))
                    {
                        maxMagnitude = magnitude;
                    }
                }
                    
                
            }
            Display(maxMagnitude, false);
        }

        public override void ReadInput()
        {
            lines = StreamReader.ReadToEnd().Split(Environment.NewLine).ToList();
        }
    }
}
