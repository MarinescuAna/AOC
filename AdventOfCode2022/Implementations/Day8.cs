namespace AdventOfCode2022.Implementations
{
    internal class Day8 : Base, IBase
    {
        private List<List<int>> _matrix = new List<List<int>>();
        public void Part1()
        {
            var sum = _matrix.Count() * 4 - 4;
            for (var line = 1; line < _matrix.Count() - 1; line++)
            {
                for (var col = 1; col < _matrix.Count() - 1; col++)
                {
                    sum +=
                        !CheckBetweenRows(line, col + 1, _matrix.Count() - col - 1, _matrix[line][col]) || //check right
                        !CheckBetweenRows(line, 0, col, _matrix[line][col]) || //check left
                        !CheckBetweenCols(col, 0, line, _matrix[line][col]) || // check up
                        !CheckBetweenCols(col, line + 1, _matrix.Count() - line - 1, _matrix[line][col]) //check down
                        ? 1 : 0;
                }
            }
            Display(resultPart1: sum.ToString());

        }
        private bool CheckBetweenRows(int line, int start, int count, int value) => Enumerable.Range(start, count).Any(i => _matrix[line][i] >= value);
        private bool CheckBetweenCols(int col, int start, int count, int value) => Enumerable.Range(start, count).Any(i => _matrix[i][col] >= value);
        private int ViewingDistanceForRows(int line, int start, int length, int value)
        {
            var distance = 0;
            if (length != 0)
            {
                for (var i = start; i < length && _matrix[line][i] <= value; i++, distance++)
                {
                    if (i < length - 1 && _matrix[line][i + 1] > value)
                    {
                        distance++;
                    }
                    if (_matrix[line][i] == value)
                    {
                        return distance+1;
                    }
                }
            }
            else
            {
                for (var i = start; i >= 0 && _matrix[line][i] <= value; i--, distance++)
                {
                    if (i > 0 && _matrix[line][i - 1] > value)
                    {
                        distance++;
                    }
                    if (_matrix[line][i] == value)
                    {
                        return distance+1;
                    }
                }
            }
            return distance;
        }
        private int ViewingDistanceCols(int col, int start, int length, int value)
        {
            var distance = 0;
            if (length != 0)
            {
                for (var i = start; i < length && _matrix[i][col] <= value; i++, distance++)
                {
                    if(i<length-1 && _matrix[i + 1][col] > value)
                    {
                        distance++;
                    }
                    if (_matrix[i][col] == value)
                    {
                        return distance + 1;
                    }
                }
            }
            else
            {
                for (var i = start; i >= 0 && _matrix[i][col] <= value; i--, distance++)
                {
                    if (i > 0 && _matrix[i - 1][col] > value)
                    {
                        distance++;
                    }
                    if (_matrix[i][col] == value)
                    {
                        return distance + 1;
                    }
                }
            }
            return distance;
        }
        public void Part2()
        {
            var max = 0;
            for (var line = 1; line < _matrix.Count() - 1; line++)
            {
                for (var col = 1; col < _matrix.Count() - 1; col++)
                {
                    var scenicScore = ViewingDistanceForRows(line, col + 1, _matrix.Count(), _matrix[line][col]) *
                                      ViewingDistanceForRows(line, col - 1, 0, _matrix[line][col]) *
                                      ViewingDistanceCols(col, line - 1, 0, _matrix[line][col]) *
                                      ViewingDistanceCols(col, line + 1, _matrix.Count(), _matrix[line][col]);

                    if (scenicScore > max)
                    {
                        max = scenicScore;
                    }
                }
            }
            Display(resultPart2: max.ToString());
        }

        public void ReadData()
        {
            _matrix = streamReader.ReadToEnd().Split(Environment.NewLine).Select(line => line.Select(no => no - '0').ToList()).ToList();
        }

        public void Run()
        {
            ReadData();
            Part1();
            Part2();
        }
    }
}
