using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day13 : BaseDay
    {
        private List<(int x, int y)> coordonates = new List<(int x, int y)>();
        private List<(int number, string ax)> folds = new List<(int number, string ax)>();
        public Day13() : base(13, "Transparent Origami")
        {
            ReadInput();
        }

        private void FoldAlong(int no, char ax)
        {
            var newCoords = ax == 'x' ? coordonates.Where(u => u.x <= no).ToList() : coordonates.Where(u => u.y <= no).ToList();
            var coordsToRemove = coordonates.Except(newCoords).ToList();
            foreach (var pair in coordsToRemove)
            {
                var newPair = ax == 'x' ? (no - (pair.x-no), pair.y) : (pair.x, no - (pair.y-no));
                if (!newCoords.Contains(newPair))
                {
                    newCoords.Add(newPair);
                }
            }
            coordonates = newCoords;


        }
        public override void Part1()
        {
            var firstFold = folds[0];
            folds.Remove(firstFold);
            if (firstFold.ax == "x")
            {
                FoldAlong(firstFold.number, 'x');
            }
            else
            {
                FoldAlong(firstFold.number, 'y');
            }
            Display(coordonates.Count().ToString(), true);
        }
        private void BuildMatrix()
        { 
            for (int i = 0; i < coordonates.Max(x => x.x); i++)
            {
                for (int j = 0; j < coordonates.Max(x => x.y); j++)
                {
                    Console.Write(coordonates.Contains((i, j)) ? '#' : '.');
                }
                Console.WriteLine();
            }
        }
        public override void Part2()
        {
            foreach (var fold in folds)
            {
              
                if (fold.ax == "x")
                {
                    FoldAlong(fold.number, 'x');
                }
                else
                {
                    FoldAlong(fold.number, 'y');
                }
            }
            BuildMatrix();
        }

        public override void ReadInput()
        {
            while ((line = StreamReader.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (!line.Contains("fold along"))
                    {
                        (int x, int y) newCoord = (int.Parse(line.Split(",")[1]), int.Parse(line.Split(",")[0]));
                        coordonates.Add(newCoord);
                    }
                    else
                    {

                        if (line.Contains("x"))
                        {
                            folds.Add((int.Parse(line.Split("=")[1]), "y"));
                        }
                        else
                        {
                            folds.Add((int.Parse(line.Split("=")[1]), "x"));
                        }
                    }
                }
            }
        }
    }
}
