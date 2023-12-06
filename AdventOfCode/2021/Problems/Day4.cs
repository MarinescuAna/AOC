using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    class Board
    {
        public bool isWin = false;
        public (int number, bool marked)[][] grid = new (int number, bool marked)[5][];
        public Board()
        {
            for (var i = 0; i < 5; i++)
            {
                grid[i] = new (int number, bool marked)[5];
            }
        }
        public int GetSum()
        {
            return grid.Sum(u => u.Where(u=>!u.marked).Sum(u => u.number));
        }
        public bool CheckRowsAndCols()
        {
            return CheckRows() || CheckColumns();
        }
        public void MarkNumber(int nr)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    if (grid[i][j].number == nr)
                    {
                        grid[i][j].marked = true;
                    }
                }
            }
        }
        private bool CheckRows()
        {
            var marked = true;
            for (var i = 0; i < 5; i++)
            {
                marked = true;
                for (var j = 0; j < 5 && marked; j++)
                {
                    marked = grid[i][j].marked;
                }

                if (marked)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckColumns()
        {
            var marked = true;
            for (var i = 0; i < 5; i++)
            {
                marked = true;
                for (var j = 0; j < 5 && marked; j++)
                {
                    marked = grid[j][i].marked;
                }

                if (marked)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Day4 : BaseDay
    {
        private List<int> drawedNumbers;
        private List<Board> boards;
        public Day4() : base(4, "Giant Squid")
        {
            boards = new List<Board>();
            drawedNumbers = new List<int>();
            ReadInput();
        }

        public override void Part1()
        {
            var winnderFound = false;
            var score = 0;

            for (var i = 0; i < drawedNumbers.Count && !winnderFound; i++)
            {
                boards.ForEach(u =>
                {
                    u.MarkNumber(drawedNumbers[i]);
                    if (u.CheckRowsAndCols())
                    {
                        winnderFound = true;
                        score = u.GetSum() * drawedNumbers[i];
                    }
                });
            }

            Display(score.ToString(),true);
        }

        public override void Part2()
        {
            var lastWinnderFound = false;
            var score = 0;
            var isWonNumber = 0;

            for (var i = 0; i < drawedNumbers.Count && !lastWinnderFound; i++)
            {
                boards.ForEach(u =>
                {
                    if (!u.isWin)
                    {
                        u.MarkNumber(drawedNumbers[i]);
                        if (u.CheckRowsAndCols())
                        {
                            if (isWonNumber == boards.Count-1)
                            {
                                lastWinnderFound = true;
                                score = u.GetSum() * drawedNumbers[i];
                            }
                            else
                            {
                                u.isWin = true;
                                isWonNumber++;
                            }
                        }
                    }
                });
            }

            Display(score.ToString(), false);
        }

        public override void ReadInput()
        {
            line = StreamReader.ReadLine();

            foreach (var nr in line.Split(","))
            {
                drawedNumbers.Add(int.Parse(nr));
            }

            line = StreamReader.ReadLine();
            var board = new Board();
            var row = 0;
            while ((line = StreamReader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    boards.Add(board);
                    board = new Board();
                    row = 0;
                }
                else
                {

                    var col = 0;
                    foreach (var nr in line.Split(" "))
                    {
                        if (!string.IsNullOrEmpty(nr))
                        {
                            board.grid[row][col++] = (int.Parse(nr), false);
                        }
                    }
                    row++;
                }
            }

            boards.Add(board);
        }
    }
}
