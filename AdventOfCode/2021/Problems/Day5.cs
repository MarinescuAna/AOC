using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AdventOfCode2021.Problems
{
    class Diagram
    {
        public int[][] grid = new int[1000][];
        public Diagram()
        {
            for (var i = 0; i < 1000; i++)
            {
                grid[i] = new int[1000];
            }
        }
        public void MarkDiagonalEqualLines(int x, int y)
        {
            var increment = 0;
            Func<int, int, bool> expression;
            if (x < y)
            {
                increment = 1;
                expression = (x, y) => x <= y;
            }
            else
            {
                increment = -1;
                expression = (x, y) => x >= y;
            }
            for (int i = x; expression(i, y); i += increment)
            {
                grid[i][i]++;
            }
        }
        public void MarkDiagonalLines(int x1, int x2, int y1, int y2)
        {

            var incrementY = 1;
            var incrementX = 1;
            Func<int, int, bool> expressionX;
            Func<int, int, bool> expressionY;
            if (x1 > x2)
            {
                incrementX = -1;
                expressionX = (x1, x2) => x1 >= x2;
            }
            else
            {
                incrementX = 1;
                expressionX = (x1, x2) => x1 <= x2;
            }

            if (y1 > y2)
            {
                incrementY = -1;
                expressionY = (y1, y2) => y1 >= y2;
            }
            else
            {
                incrementY = 1;
                expressionY = (y1, y2) => y1 <= y2;
            }

            for (int x = x1, y = y1; expressionX(x, x2) && expressionY(y, y2); x += incrementX, y += incrementY)
            {
                grid[x][y]++;
            }
        }
        public void MarkVerticalLine(int x, int y1, int y2)
        {
            var increment = 0;
            Func<int, int, bool> expression;
            if (y1 < y2)
            {
                increment = 1;
                expression = (x, y) => x <= y;
            }
            else
            {
                increment = -1;
                expression = (x, y) => x >= y;
            }

            for (int i = y1; expression(i, y2); i += increment)
            {
                grid[x][i]++;
            }
        }
        public void MarkHorizontalLine(int y, int x1, int x2)
        {
            var increment = 0;
            Func<int, int, bool> expression;
            if (x1 < x2)
            {
                increment = 1;
                expression = (x, y) => x <= y;
            }
            else
            {
                increment = -1;
                expression = (x, y) => x >= y;
            }
            for (int i = x1; expression(i, x2); i += increment)
            {
                grid[i][y]++;
            }
        }
        public int CountDangerousZones()
        {
            var count = 0;
            foreach (var line in grid)
            {
                count += line.Where(u => u > 1).ToList().Count;
            }

            return count;
        }
    }
    public class Day5 : BaseDay
    {
        private Diagram Diagram;
        private List<(int x1, int y1, int x2, int y2)> points;
        public Day5() : base(5, "Hydrothermal Venture")
        {
            Diagram = new Diagram();
            points = new List<(int x1, int y1, int x2, int y2)>();
            ReadInput();
        }

        public override void Part1()
        {
            foreach (var point in points.Where(u => u.x1 == u.x2 || u.y1 == u.y2).ToList())
            {
                if (point.y1 == point.y2)
                {
                    Diagram.MarkVerticalLine(
                       point.y1,
                       point.x1 < point.x2 ? point.x1 : point.x2,
                       point.x1 > point.x2 ? point.x1 : point.x2
                       );
                }
                else
                {
                    Diagram.MarkHorizontalLine(
                        point.x1,
                        point.y1 < point.y2 ? point.y1 : point.y2,
                        point.y1 > point.y2 ? point.y1 : point.y2
                        );

                }

            }
            Display(Diagram.CountDangerousZones().ToString(), true);
        }

        public override void Part2()
        {
            foreach (var point in points)
            {
                if (point.x1 == point.y1 && point.x2 == point.y2)
                {
                    Diagram.MarkDiagonalEqualLines(point.x1, point.x2);
                }
                else if (point.y1 == point.y2)
                {
                    Diagram.MarkVerticalLine(
                       point.y1,
                       point.x1 < point.x2 ? point.x1 : point.x2,
                       point.x1 > point.x2 ? point.x1 : point.x2
                       );
                }
                else if (point.x1 == point.x2)
                {
                    Diagram.MarkHorizontalLine(
                        point.x1,
                        point.y1 < point.y2 ? point.y1 : point.y2,
                        point.y1 > point.y2 ? point.y1 : point.y2
                        );

                }
                else
                {
                    Diagram.MarkDiagonalLines(point.y1, point.y2, point.x1, point.x2);
                }
            }
            Display(Diagram.CountDangerousZones().ToString(), false);
        }
        private int ConvertToInt(string nr)
        {
            return int.Parse(nr);
        }
        public override void ReadInput()
        {
            points = StreamReader.ReadToEnd().Split(Environment.NewLine).Select(u =>
                    (
                        ConvertToInt(u.Split(" -> ")[0].Split(",")[0]),
                        ConvertToInt(u.Split(" -> ")[0].Split(",")[1]),
                        ConvertToInt(u.Split(" -> ")[1].Split(",")[0]),
                        ConvertToInt(u.Split(" -> ")[1].Split(",")[1])
                    )).ToList();
        }
    }
}
