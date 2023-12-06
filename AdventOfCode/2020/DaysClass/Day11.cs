using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day11Process
    {
        private static Day11 Day = new Day11();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day11
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In11.txt");
        private char[][] map = new char[1000][];
        private int lines = 0;
        private int columns = 0;
        public Day11()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;

            while ((line = file.ReadLine()) != null)
            {
                var col = 0;
                map[lines] = new char[1000];
                foreach (var l in line.Replace('L', '#'))
                {
                    map[lines][col++] = l;
                }
                columns = col;
                lines++;
            }

        }
        private bool FreeSeat(int line, int col)
        {
            var countOccupiedSeats = 0;
            if ((line == 0 && col == 0) ||
                (line == 0 && col == columns - 1) ||
                (line == lines - 1 && col == 0) ||
                (line == lines - 1 && col == columns - 1))
            {
                return false;
            }
            else
            {
                if (line == 0 && col != 0 && col != columns - 1)
                {
                    if (map[line][col - 1] == '#')
                    {
                        countOccupiedSeats++;
                    }
                    if (map[line][col + 1] == '#')
                    {
                        countOccupiedSeats++;
                    }
                    if (map[line + 1][col - 1] == '#')
                    {
                        countOccupiedSeats++;
                    }
                    if (map[line + 1][col] == '#')
                    {
                        countOccupiedSeats++;
                    }
                    if (map[line + 1][col + 1] == '#')
                    {
                        countOccupiedSeats++;
                    }
                }
                else
                {
                    if (line == lines - 1 && col != 0 && col != columns - 1)
                    {
                        if (map[line - 1][col - 1] == '#')
                        {
                            countOccupiedSeats++;
                        }
                        if (map[line - 1][col] == '#')
                        {
                            countOccupiedSeats++;
                        }
                        if (map[line - 1][col + 1] == '#')
                        {
                            countOccupiedSeats++;
                        }
                        if (map[line][col - 1] == '#')
                        {
                            countOccupiedSeats++;
                        }
                        if (map[line][col + 1] == '#')
                        {
                            countOccupiedSeats++;
                        }
                    }
                    else
                    {
                        if (col == 0 && line != 0 && line != lines - 1)
                        {

                            if (map[line - 1][col] == '#')
                            {
                                countOccupiedSeats++;
                            }
                            if (map[line - 1][col + 1] == '#')
                            {
                                countOccupiedSeats++;
                            }

                            if (map[line][col + 1] == '#')
                            {
                                countOccupiedSeats++;
                            }
                            if (map[line + 1][col] == '#')
                            {
                                countOccupiedSeats++;
                            }
                            if (map[line + 1][col + 1] == '#')
                            {
                                countOccupiedSeats++;
                            }
                        }
                        else
                        {
                            if (col == columns - 1 && line != 0 && line != lines - 1)
                            {
                                if (map[line - 1][col - 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line - 1][col] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line][col - 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line + 1][col - 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line + 1][col] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                            }
                            else
                            {
                                if (map[line - 1][col - 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line - 1][col] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line - 1][col + 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line][col - 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line][col + 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line + 1][col - 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line + 1][col] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                                if (map[line + 1][col + 1] == '#')
                                {
                                    countOccupiedSeats++;
                                }
                            }
                        }
                    }
                }
            }
            return countOccupiedSeats >= 4;
        }
        private bool OccupSeat(int line, int col)
        {
            if (line == 0 &&
                col == 0 &&
                map[line][col + 1] != '#' &&
                map[line + 1][col] != '#' &&
                map[line + 1][col + 1] != '#')
            {
                return true;
            }
            else
            {
                if (line == 0 &&
                    col != 0 &&
                    col != columns - 1 &&
                    map[line][col - 1] != '#' &&
                    map[line][col + 1] != '#' &&
                    map[line + 1][col - 1] != '#' &&
                    map[line + 1][col] != '#' &&
                    map[line + 1][col + 1] != '#')
                {
                    return true;
                }
                else
                {
                    if (line == 0 &&
                        col == columns - 1 &&
                        map[line][col - 1] != '#' &&
                        map[line + 1][col - 1] != '#' &&
                        map[line + 1][col] != '#')
                    {
                        return true;
                    }
                    else
                    {
                        if (line != 0 &&
                            col == 0 &&
                            map[line - 1][col] != '#' &&
                            map[line - 1][col + 1] != '#' &&
                            map[line][col + 1] != '#' &&
                            map[line + 1][col] != '#' &&
                            map[line + 1][col + 1] != '#')
                        {
                            return true;
                        }
                        else
                        {
                            if (line != 0 &&
                                col == columns - 1 &&
                                map[line - 1][col - 1] != '#' &&
                                map[line - 1][col] != '#' &&
                                map[line][col - 1] != '#' &&
                                map[line + 1][col - 1] != '#' &&
                                map[line + 1][col] != '#')
                            {
                                return true;
                            }
                            else
                            {
                                if (line == lines - 1 &&
                                    col == 0 &&
                                    map[line - 1][col] != '#' &&
                                    map[line - 1][col + 1] != '#' &&
                                    map[line][col + 1] != '#')
                                {
                                    return true;
                                }
                                else
                                {
                                    if (line == lines - 1 &&
                                        col != 0 &&
                                        map[line - 1][col - 1] != '#' &&
                                        map[line - 1][col] != '#' &&
                                        map[line - 1][col + 1] != '#' &&
                                        map[line][col - 1] != '#' &&
                                        map[line][col + 1] != '#')
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        if (line == lines - 1 &&
                                            col == columns - 1 &&
                                            map[line - 1][col - 1] != '#' &&
                                            map[line - 1][col] != '#' &&
                                            map[line][col - 1] != '#')
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            if (line != 0 &&
                                               col != 0 &&
                                               line != lines - 1 &&
                                               col != columns - 1 &&
                                               map[line - 1][col - 1] != '#' &&
                                               map[line - 1][col] != '#' &&
                                               map[line - 1][col + 1] != '#' &&
                                               map[line][col - 1] != '#' &&
                                               map[line][col + 1] != '#' &&
                                               map[line + 1][col - 1] != '#' &&
                                               map[line + 1][col] != '#' &&
                                               map[line + 1][col + 1] != '#')
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        private void printMap(char[][] matrix)
        {

            for (var i = 0; i < lines; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Console.Write($"{ matrix[i][j]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private void copyMatrix(char[][] matrix, char[][] matrix2)
        {
            for (var i = 0; i < lines; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    matrix[i][j] = matrix2[i][j];
                }
            }
        }
        public void Part1()
        {
            var changeState = true;
            var tempMatrix = new char[lines + 1][];

            for (var i = 0; i < lines; i++)
            {
                tempMatrix[i] = new char[columns + 1];
            }

            while (changeState)
            {
                changeState = false;

                copyMatrix(tempMatrix, map);
                for (var l = 0; l < lines; l++)
                {
                    for (var c = 0; c < columns; c++)
                    {
                        if (map[l][c] == '#' && FreeSeat(l, c))
                        {
                            changeState = true;
                            tempMatrix[l][c] = 'L';
                        }
                        else
                        {
                            if (map[l][c] == 'L' && OccupSeat(l, c))
                            {
                                changeState = true;
                                tempMatrix[l][c] = '#';
                            }
                        }

                    }

                }
                copyMatrix(map, tempMatrix);
            }

            var occupiedSeats = 0;
            for (var l = 0; l < lines; l++)
            {
                for (var c = 0; c < columns; c++)
                {
                    if (map[l][c] == '#')
                    {
                        occupiedSeats++;
                    }
                }
            }

            Console.WriteLine("Day 11: Seating System {0} (part1)", occupiedSeats);

        }
        public void Part2()
        {
            var frequency = new long[200];

            for (var i = 0; i < lines; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (map[i][j] != '.')
                    {
                        map[i][j] = 'L';
                    }
                }
            }


            Console.WriteLine("Day 11: Seating System {0} (part2)", frequency[0]);
        }
    }
}
