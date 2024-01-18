using System.Linq;

namespace AdventOfCode2023.Day13
{
    public class Solution : Base
    {
        public override void Run()
        {
            var patterns = streamReader.ReadToEnd().Split("\r\n\r\n");
            foreach (var pattern in patterns)
            {
                var splitedPattern = pattern.Split(Environment.NewLine).ToList();
                var lineOfReflaction = FindLineOfReflection(splitedPattern);
            }
        }

        private (int first, int sec, bool isVertical) FindLineOfReflection(List<string> pattern)
        {
            //search vertically
            var firstMatch = new List<int>();
            for (var col = 0; col < pattern[0].Length; col++)
            {
                var match = firstMatch.Contains(col) ? firstMatch.IndexOf(col) : 0;

                for (var col2 = col + 1; col2 < pattern[0].Length && match == 0; col2++)
                    if (MatchColumns(col, col2, pattern))
                        match = col2;

                firstMatch.Add(match == 0 ? -1 : match);
            }

            

            var verticallyFirstIndex = FindReflexion(firstMatch);
            firstMatch.Clear();
            //search horizontally 
            for (var row = 0; row < pattern.Count; row++)
            {
                var match = firstMatch.Contains(row) ? firstMatch.IndexOf(row) : 0;
                for (var row1 = row + 1; row1 < pattern.Count && match == 0; row1++)
                    if (MatchRows(row, row1, pattern))
                        match = row1;

                firstMatch.Add(match == 0 ? -1 : match);
            }

            return (-1, -1, false);
        }

        private (int, int)? FindReflexion(List<int> matches)
        {
            //skip -1 values
            var index = 0;
            while (index < matches.Count && matches[index] == -1) index++;
            var countMatches = 0;
            while(index < matches.Count - 1 && matches[index] > matches[index+1])
            {
                index++;
                countMatches++;
            }

            return (index, countMatches);
        }
        private bool MatchColumns(int first, int sec, List<string> pattern)
        {
            for (var line = 0; line < pattern.Count; line++)
            {
                if (pattern[line][first] != pattern[line][sec])
                    return false;
            }

            return true;
        }

        private bool MatchRows(int first, int sec, List<string> pattern)
            => pattern[first].Equals(pattern[sec]);
    }
}
