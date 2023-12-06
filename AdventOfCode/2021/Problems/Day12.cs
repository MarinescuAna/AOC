using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day12 : BaseDay
    {
        private Dictionary<string, List<string>> Nodes = new Dictionary<string, List<string>>();
        private List<string> paths = new List<string>();
        public Day12() : base(12, "Passage Pathing")
        {
            ReadInput();
        }

        private void FindPaths(string node, string path)
        {
            if (node == "end")
            {
                paths.Add(path);
            }
            else
            {

                foreach (var nod in Nodes[node])
                {
                    if (nod != "start")
                    {
                        if (nod.Any(Char.IsLower))
                        {
                            if (!path.Contains(nod))
                            {
                                FindPaths(nod, path + "," + nod);
                            }
                        }
                        else
                        {
                            FindPaths(nod, path + "," + nod);
                        }
                    }
                }
            }
        }

        public override void Part1()
        {
            FindPaths("start", "start");
            Display(paths.Count().ToString(), true);
        }

        private void FindPathsPart2(string node, string path, bool haveTwiceLower)
        {
            if (node == "end")
            {
                if (!paths.Contains(path))
                {
                    paths.Add(path);
                }
            }
            else
            {
                foreach (var nod in Nodes[node])
                {
                    if (nod != "start")
                    {
                        if (nod.Any(Char.IsLower))
                        {
                            if (!path.Contains(nod) || (path.Contains(nod) && !haveTwiceLower))
                            {
                                FindPathsPart2(nod, path + "," + nod, path.Contains(nod) && !haveTwiceLower ? true : haveTwiceLower);
                            }
                        }
                        else
                        {
                            FindPathsPart2(nod, path + "," + nod, haveTwiceLower);
                        }
                    }
                }
            }
        }
        public override void Part2()
        {
            FindPathsPart2("start", "start", false);
            Display(paths.Count().ToString(), true);
        }

        public override void ReadInput()
        {
            var nodes = StreamReader.ReadToEnd().Split(Environment.NewLine).ToList();
            foreach (var node in nodes.Select(u => u.Split("-")).ToList())
            {
                for (var i = 0; i < 2; i++)
                {
                    if (!Nodes.ContainsKey(node[i]))
                    {
                        Nodes.Add(node[i], new List<string>());
                    }
                }

            }

            foreach (var node in Nodes)
            {
                foreach (var n in nodes.Where(n => n.Split("-")[0] == node.Key || n.Split("-")[1] == node.Key))
                {
                    Nodes[node.Key].Add(n.Replace("-", "").Replace(node.Key, ""));
                }
            }
        }
    }
}
