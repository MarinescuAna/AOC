using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day20Process
    {
        private static Day20 Day20 = new Day20();
        public static void Process()
        {
            Day20.Part1();
            Day20.Part2();
        }
    }
    public class Tile
    {
        public int Id;
        public List<List<(int, int)>> Positions = new List<List<(int, int)>>();
        public Tile(int id)
        {
            Id = id;
        }
        public void Flip()
        {

        }
    }
    public class Day20
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In20.txt");
        private List<Tile> tiles = new List<Tile>();
        public Day20()
        {
            ReadInput();
        }

        private List<List<(int,int)>> FindPositions(string[] tile)
        {
            var list = new List<List<(int, int)>>();
            var line = new List<(int, int)>();
            for(var i = 0; i < 10; i++)
            {
                if (tile[0][i] == '#')
                {
                    line.Add((0, i));
                }
            }
            list.Add(line);
            line.Clear();
            for (var i = 0; i < 10; i++)
            {
                if (tile[9][i] == '#')
                {
                    line.Add((9, i));
                }
            }
            list.Add(line);
            line.Clear();
            for (var i = 0; i < 10; i++)
            {
                if (tile[i][0] == '#')
                {
                    line.Add((i, 0));
                }
            }
            list.Add(line);
            line.Clear();
            for (var i = 0; i < 10; i++)
            {
                if (tile[i][9] == '#')
                {
                    line.Add(( i,9));
                }
            }
            list.Add(line);
            return list;
        }
        private void ReadInput()
        {
            var line = string.Empty;
            var lines = 0;
            var tile = new string[10];
            Tile tile1=null;
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    tile1.Positions = FindPositions(tile);
                    tiles.Add(tile1);
                    Array.Clear(tile,0,tile.Length);
                    lines = 0;
                    tile1 = null;
                }
                else
                if (line.Contains("Tile"))
                {
                    tile1 =new Tile(int.Parse(line.Replace("Tile ", "").Replace(":", "")));
                }
                else
                {
                    tile[lines++]=line;
                }
            }
            tile1.Positions = FindPositions(tile);
            tiles.Add(tile1);
        }
        private bool UpOrDownMarginMatch(string[] tile1, string[] til2, int line)
        {
            var line2 = line==0?9:0;
            var isMatch = true;
          
            for (var j = 0; j < tile1[0].Length && isMatch; j++)
            {
                if (tile1[line][j] != til2[line2][j])
                {
                    isMatch = false;
                }
            }

            if (!isMatch)
            {
                isMatch = true;
                for (var j = 0; j < tile1.Length && isMatch; j++)
                {
                    if (tile1[line][j] != til2[line2][9-j])
                    {
                        isMatch = false;
                    }
                }
            }
            return isMatch;

        }
        private bool LeftOrRightMarginMatch(string[] tile1, string[] til2, int col)
        {
            var col2 = col==0?9:0;
            var isMatch = true;
            for (var j = 0; j < tile1.Length; j++)
            {
                if (tile1[j][col] != til2[j][col2])
                {
                    isMatch = false;
                }
            }
            if (!isMatch)
            {
                isMatch = true;
                for (var j = 0; j < tile1.Length; j++)
                {
                    if (tile1[j][col] != til2[9-j][col2])
                    {
                        isMatch = false;
                    }
                }
            }
            return isMatch;

        }
        public void Part1()
        {
            var prod = 1L;
            for(var i=0;i< tiles.Count;i++)
            {
                var freqConner = new int[5];
                //1->up
                //2->down
                //3->right
                //4->left
                for(var j=0;j<9;j++)
                {
                   /* if (j != i)
                    {
                        if (UpOrDownMarginMatch(tiles[i], tiles[j], 0))
                        {
                            freqConner[1]++;
                        }
                        if (UpOrDownMarginMatch(tiles[i], tiles[j], tiles.Count - 1))
                        {
                            freqConner[2]++;
                        }
                        if (LeftOrRightMarginMatch(tiles[i], tiles[j], 0))
                        {
                            freqConner[4]++;
                        }
                        if (LeftOrRightMarginMatch(tiles[i], tiles[j], tiles[0].Length - 1))
                        {
                            freqConner[3]++;
                        }
                    }*/
                }
                var notMatch = 0;
                for (var k = 1; k <= 4; k++)
                {
                    if (freqConner[k] == 0)
                    {
                        notMatch++;
                    }
                }
                if (notMatch == 2)
                {
                    //prod *= ids[i];
                }
            }
            Console.WriteLine("Day 16: Ticket Translation {0} (part1)", prod);
        }
          public void Part2()
        {
            
    
            //Console.WriteLine("Day 16: Ticket Translation {0} (part2)", prod);
        }
    }

}
