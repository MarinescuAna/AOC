using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day20 : BaseDay
    {
        private string ImageEnhancementAlg = string.Empty;
        private Dictionary<(int X, int Y), bool> InputImage = new Dictionary<(int X, int Y), bool>();
        private Dictionary<(int X, int Y), bool> InputImage2 = new Dictionary<(int X, int Y), bool>();
        private (int i, int j)[] Neighbors = { (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1), };
        public Day20() : base(20, "Trench Map")
        {
            ReadInput();
        }
        private bool IsLit((int X, int Y) coord, int minX)
        {
            if (!InputImage.ContainsKey(coord) && minX % 2 == 0 && ImageEnhancementAlg[0] == '#')
            {
                return true;
            }
            if (!InputImage.ContainsKey(coord))
            {
                return false;
            }
            return InputImage[coord];
        }
        private void Expand()
        {
            var minX = InputImage.Keys.Min(x => x.X);
            var minY = InputImage.Keys.Min(x => x.Y);
            var maxX = InputImage.Keys.Max(x => x.X);
            var maxY = InputImage.Keys.Max(x => x.Y);
            var OutputImage = new Dictionary<(int X, int Y), bool>();

            for (int y = minY - 1; y <= maxY + 1; y++)
            {
                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    var miniGrid = Neighbors.Select(z => (z.i + x, z.j + y)).ToList();
                    var binary = new string(miniGrid.Select(z => IsLit(z, minX + 1) ? '1' : '0').ToArray());
                    OutputImage[(x, y)] = ImageEnhancementAlg[Convert.ToInt32(binary, 2)] == '#';
                }
            }

            InputImage = OutputImage;
      
        }
        public override void Part1()
        {
            Enumerable.Range(0, 2).ToList().ForEach(x => Expand());
            Display(InputImage.Where(u => u.Value).Count().ToString(), true);
        }

        public override void Part2()
        {
            InputImage = InputImage2;
            Enumerable.Range(0, 50).ToList().ForEach(x => Expand());
            Display(InputImage.Where(u => u.Value).Count().ToString(), true);
        }

        public override void ReadInput()
        {
            var input = StreamReader.ReadToEnd().Split(Environment.NewLine);
            ImageEnhancementAlg = input[0];
            InputImage = input[2..].SelectMany((x, i) => x.Select((y, j) => (j, i, y))).ToDictionary(x => (x.j, x.i), x => x.y == '#');
            InputImage2 = InputImage;
        }
    }
}
