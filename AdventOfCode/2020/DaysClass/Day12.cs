using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day12Process
    {
        private static Day12 Day = new Day12();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
    public class Day12
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\marin\OneDrive\Documente\GitHub\AOC-2020\DaysText\In12.txt");
        private List<(char,int)> instractions = new List<(char,int)>();
 
        public Day12()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;

            while ((line = file.ReadLine()) != null)
            {
                var direction = line[0];
                var degreeOrUnits = int.Parse(line.Replace(direction, ' '));
                instractions.Add((direction,degreeOrUnits));            
            }

        }
      
        public void Part1()
        { 
            var currentPosition = 0;
            var coord = (0,0);
            //0->E
            //1->S
            //2->W
            //3->N

            foreach(var instraction in instractions)
            {
                switch (instraction.Item1){
                    case 'F':
                        {
                            switch (currentPosition)
                            {
                                case 0:
                                    coord = (coord.Item1 + instraction.Item2, coord.Item2);
                                    break;
                                case 1:
                                    coord = (coord.Item1, coord.Item2 - instraction.Item2);
                                    break;
                                case 2:
                                    coord = (coord.Item1 - instraction.Item2, coord.Item2);
                                    break;
                                case 3:
                                    coord = (coord.Item1, coord.Item2 + instraction.Item2);
                                    break;
                            }
                            break;
                        }
                    case 'N':
                        {
                            coord = (coord.Item1, coord.Item2 + instraction.Item2);
                            break;
                        }
                    case 'W':
                        {
                            coord = (coord.Item1-instraction.Item2, coord.Item2);
                            break;
                        }
                    case 'S':
                        {
                            coord = (coord.Item1, coord.Item2 - instraction.Item2);
                            break;
                        }
                    case 'E':
                        {
                            coord = (coord.Item1+ instraction.Item2, coord.Item2 );
                            break;
                        }
                    case 'R':
                        {
                            currentPosition = (currentPosition + instraction.Item2 / 90) % 4;
                            break;
                        }
                    case 'L':
                        {
                            currentPosition =Math.Abs((currentPosition - instraction.Item2 / 90 ) + 4)%4;
                            break;
                        }

                }
            }

            Console.WriteLine("Day 12: Rain Risk {0} (part1)", Math.Abs(coord.Item1) + Math.Abs(coord.Item2));

        }
        public void Part2()
        {
            var coord = (0, 0);
            var waypoint = (10, 1);

            foreach (var instraction in instractions)
            {
                switch (instraction.Item1)
                {
                    case 'F':
                        {
                            for (int j = 0; j < instraction.Item2; j++)
                            {
                                coord = (coord.Item1 + waypoint.Item1, coord.Item2 + waypoint.Item2);
                            }
                            break;
                        }
                    case 'N':
                        {
                            waypoint = (waypoint.Item1, waypoint.Item2 + instraction.Item2);
                            break;
                        }
                    case 'W':
                        {
                            waypoint = (waypoint.Item1 - instraction.Item2, waypoint.Item2);
                            break;
                        }
                    case 'S':
                        {
                            waypoint = (waypoint.Item1, waypoint.Item2 - instraction.Item2);
                            break;
                        }
                    case 'E':
                        {
                            waypoint = (waypoint.Item1 + instraction.Item2, waypoint.Item2);
                            break;
                        }
                    case 'R':
                        {
                            for (int j = 0; j < instraction.Item2; j += 90)
                            {
                                waypoint = Right(waypoint);
                            }
                            break;
                        }
                    case 'L':
                        {
                            for (int j = 0; j < instraction.Item2; j += 90)
                            {
                                waypoint = Left(waypoint);
                            }
                            break;
                        }

                }
            }

            Console.WriteLine("Day 12: Rain Risk {0} (part2)", Math.Abs(coord.Item1)+Math.Abs(coord.Item2));
        }

        private (int, int) Right((int, int) way)
        {
            return (way.Item2, -way.Item1);
        }

        private (int, int) Left((int, int) way)
        {
            return Right(Right(Right(way)));
        }
    }
}
