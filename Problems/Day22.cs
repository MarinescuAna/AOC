using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day22 : BaseDay
    {
        private List<(int minx, int miny, int minz, int maxx, int maxy, int maxz, bool isOn)> RebootSteps = new List<(int minx, int miny, int minz, int maxx, int maxy, int maxz, bool isOn)>();
        private Dictionary<(int x, int y, int z), bool> CubesOn = new Dictionary<(int x, int y, int z), bool>();
        public Day22() : base(22, "Reactor Reboot")
        {
            ReadInput();
        }
        private void CubeGeneration((int x, int y, int z) min, (int x, int y, int z) max, bool remove)
        {
           for(var x = min.x; x <= max.x; x++)
            {
                for (var y = min.y; y <= max.y; y++)
                {
                    for (var z = min.z; z <= max.z; z++)
                    {
                        if (remove)
                        {
                            if (CubesOn.ContainsKey((x, y, z)))
                            {
                                CubesOn.Remove((x, y, z));
                            }
                        }
                        else
                        {
                            if (!CubesOn.ContainsKey((x, y, z)))
                            {
                                CubesOn.Add((x, y, z),true);
                            }
                        }
                    }
                }
            }

           
        }
        public override void Part1()
        {
            RebootSteps
                .Where(t => t.minx >= -50 && t.miny >= -50 && t.minz >= -50 && t.maxx <= 50 && t.maxy <= 50 && t.maxz <= 50)
                .ToList()
                .ForEach(x => CubeGeneration((x.minx, x.miny, x.minz), (x.maxx, x.maxy, x.maxz), !x.isOn));

            Display(CubesOn.Count().ToString(), true);
        }

        public override void Part2()
        {
            CubesOn.Clear();
            RebootSteps
                .ToList()
                .ForEach(x => CubeGeneration((x.minx, x.miny, x.minz), (x.maxx, x.maxy, x.maxz), !x.isOn));

            Display(CubesOn.Count().ToString(), true);
        }

        public override void ReadInput()
        {
            StreamReader.ReadToEnd().Split(Environment.NewLine).ToList().ForEach(line =>
               {
                   var isOn = line.Contains("on");
                   var xyz = line.Replace(isOn ? "on " : "off ", "").Replace("x=", "").Replace("y=", "").Replace("z=", "").Split(",");
                   RebootSteps.Add((
                       int.Parse(xyz[0].Split("..")[0]),
                       int.Parse(xyz[1].Split("..")[0]),
                       int.Parse(xyz[2].Split("..")[0]),
                       int.Parse(xyz[0].Split("..")[1]),
                       int.Parse(xyz[1].Split("..")[1]),
                       int.Parse(xyz[2].Split("..")[1]),
                       isOn
                       ));
               });
        }
    }
}
