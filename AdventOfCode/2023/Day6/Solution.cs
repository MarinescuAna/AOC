using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day6
{
    public class Solution : Base
    {
        private void Part1(string[] input)
        {
            var records = Regex.Matches(input[0], "\\d+").Select(m => long.Parse(m.Value)).Zip(Regex.Matches(input[1], "\\d+").Select(m => long.Parse(m.Value)), (x, y) => (x, y)).ToList();
            Console.WriteLine(records.Aggregate(1, (buffer, record) => buffer * ComputeWays(record)));
        }

        private void Part2(string[] input)
        {
            var time = long.Parse(input[0].Replace("Time: ", "").Replace(" ", ""));
            var distance = long.Parse(input[1].Replace("Distance: ", "").Replace(" ", ""));
            Console.WriteLine(ComputeWays((time, distance)));
        }

        private int ComputeWays((long timeMilliseconds, long distanceMillimeters) record)
        {
            var validOptions = 0;

            for (var ms = 1; ms < record.timeMilliseconds; ms++)
            {
                if ((record.timeMilliseconds - ms) * ms > record.distanceMillimeters)
                    validOptions++;
            }
            return validOptions;
        }
        public override void Run()
        {
            var input = streamReader.ReadToEnd().Split('\n');

            Part1(input);
            Part2(input);
        }
    }
}
