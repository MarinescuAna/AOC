namespace AdventOfCode2023.Day5
{
    enum IntervalPosition
    {
        None,
        Start,
        Inner,
        Outer,
        End
    }
    public class Solution : Base
    {
        private void Part1(string[] input)
        {
            var seeds = input.First().Replace("seeds: ", "").Split(" ").Select(v => (long.Parse(v), false)).ToList();

            for (var i = 1; i < input.Length; i++)
            {
                if (input[i].Contains("map:"))
                {
                    for (var j = 0; j < seeds.Count && seeds.Any(v => v.Item2); j++)
                    {
                        seeds[j] = (seeds[j].Item1, false);
                    }
                    while (i + 1 < input.Length && !string.IsNullOrWhiteSpace(input[++i]))
                    {
                        //0 destination
                        //1 source
                        //2 length
                        var row = input[i].Split(" ").Select(long.Parse).ToList();
                        foreach (var seed in seeds.Where(s => s.Item1 >= row[1] && s.Item1 < row[1] + row[2]).ToList())
                        {
                            if (!seed.Item2)
                            {
                                //(number - start_source) + start_destination <=> (number - row[1]) + row[0]
                                seeds.Remove(seed);
                                seeds.Add((seed.Item1 - row[1] + row[0], true));
                            }
                        }
                    }
                }
            }


            Console.WriteLine(seeds.Min(v => v.Item1));
        }
        private void Part2(string[] input)
        {
            List<(long start, long end, bool wasChanged)> seedIntervals = input.First().Replace("seeds: ", "").Split(" ").Select((num, index) => new { num, index })
                                .GroupBy(x => x.index / 2)
                                .Select(group => (long.Parse(group.ElementAt(0).num), long.Parse(group.ElementAt(0).num) + long.Parse(group.ElementAt(1).num) - 1, false))
                                .ToList();

            for (var i = 1; i < input.Length; i++)
            {
                if (input[i].Contains("map:"))
                {
                    for (var j = 0; j < seedIntervals.Count && seedIntervals.Any(v => v.wasChanged); j++)
                    {
                        seedIntervals[j] = (seedIntervals[j].start, seedIntervals[j].end, false);
                    }

                    while (i + 1 < input.Length && !string.IsNullOrWhiteSpace(input[++i]))
                    {
                        var row = input[i].Split(" ").Select(long.Parse).ToList();
                        (long start, long end) destinationInterval = (row[0], row[0] + row[2] - 1);
                        (long start, long end) sourceInterval = (row[1], row[1] + row[2] - 1);
                        var lengthGivenInterval = row[2];
                        foreach (var seed in seedIntervals.Where(s => !s.wasChanged && InRange((s.start, s.end), sourceInterval) != IntervalPosition.None).ToList())
                        {
                            seedIntervals.Remove(seed);
                            switch (InRange((seed.start, seed.end), sourceInterval))
                            {
                                case IntervalPosition.Start:
                                    //first interval which is not included
                                    seedIntervals.Add((seed.start, sourceInterval.start - 1, false));
                                    //second interval
                                    var numbersLeft = seed.end - sourceInterval.start;
                                    seedIntervals.Add((destinationInterval.start, destinationInterval.start + numbersLeft, true));
                                    break;
                                case IntervalPosition.Inner:
                                    var position = seed.start - sourceInterval.start;
                                    var startInterval = destinationInterval.start + position;
                                    seedIntervals.Add((startInterval, startInterval + seed.end - seed.start, true));
                                    break;
                                case IntervalPosition.End:
                                    //first interval which is contained by the category
                                    var positionStart = seed.start - sourceInterval.start;
                                    seedIntervals.Add((destinationInterval.start + positionStart, destinationInterval.end, true));
                                    //last interval
                                    seedIntervals.Add((seed.start + lengthGivenInterval - positionStart, seed.end, false));
                                    break;
                                case IntervalPosition.Outer:
                                    //first two not contained
                                    seedIntervals.Add((seed.start, sourceInterval.start - 1, false));
                                    seedIntervals.Add((sourceInterval.end + 1, seed.end, false));
                                    seedIntervals.Add((destinationInterval.start, destinationInterval.end, true));
                                    break;
                                default:
                                    seedIntervals.Add(seed);
                                    break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(seedIntervals.Min(v => v.start));
        }
        private IntervalPosition InRange((long start, long end) intervalSeeds, (long start, long end) intervalCategory)
        {
            if (intervalSeeds.start >= intervalCategory.start && intervalSeeds.end <= intervalCategory.end)
                //--{--[seeds]--}----
                return IntervalPosition.Inner;
            else if (intervalSeeds.start < intervalCategory.start && intervalSeeds.end > intervalCategory.end)
                //--[seeds{seeds}seeds]----
                return IntervalPosition.Outer;
            else if (intervalSeeds.start < intervalCategory.start && intervalSeeds.end <= intervalCategory.end && intervalSeeds.end >= intervalCategory.start)
                //--[-seeds-{--seeds]--}----
                return IntervalPosition.Start;
            else if (intervalSeeds.start >= intervalCategory.start && intervalSeeds.end > intervalCategory.end && intervalSeeds.start <= intervalCategory.end)
                //--{--[seeds--}---]----
                return IntervalPosition.End;
            else
                return IntervalPosition.None;
        }
        public override void Run()
        {
            var input = streamReader.ReadToEnd().Split(Environment.NewLine);
            Part1(input);
            Part2(input);
        }
    }
}
