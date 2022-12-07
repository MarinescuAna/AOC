using System.Security;

namespace AdventOfCode2022.Implementations
{
    internal class Day7 : Base, IBase
    {
        private List<string> _commands = new List<string>();
        public void Part1()
        {
            var systemFiles = new Dictionary<string, List<long>>();
            ExecuteCommands(systemFiles);
        }

        public void Part2()
        {
            throw new NotImplementedException();
        }

        public void ReadData()
        {
            _commands = streamReader.ReadToEnd().Split(Environment.NewLine).ToList();
        }

        public void Run()
        {
            ReadData();
            Part1();
        }
        private void ExecuteCommands(Dictionary<string, List<long>> tree)
        {
            var currentRoot = "/";
            var temporaryRoot = "";
            for (var i = 1; i < _commands.Count(); i++)
            {
                var splitCommand = _commands[i].Split(" ").ToList();

                if (_commands[i].Contains("$ cd"))
                {
                    temporaryRoot = string.Empty;
                    currentRoot = splitCommand[2] switch
                    {
                        ".." => GoOutLevel(currentRoot),
                        "/" => "/",
                        _ => currentRoot == "/" ? currentRoot + splitCommand[2] : $"{currentRoot}/{splitCommand[2]}"
                    };
                }
                if (splitCommand[0].All(u => char.IsDigit(u)))
                {
                    temporaryRoot = string.IsNullOrEmpty(temporaryRoot)? currentRoot : temporaryRoot;
                    if (tree.ContainsKey(temporaryRoot))
                    {
                        tree[temporaryRoot].Add(long.Parse(splitCommand[0]));
                    }
                    else
                    {
                        tree.Add(temporaryRoot, new List<long>
                        {
                            long.Parse(splitCommand[0])
                        });
                    }
                }
                if (_commands[i].Contains("dir "))
                {
                    temporaryRoot = currentRoot == "/" ? currentRoot + splitCommand[1] : $"{currentRoot}/{splitCommand[1]}";
                }
                if (_commands[i].Contains("$ ls"))
                {
                    temporaryRoot = string.Empty;
                }
            }
        }
        private string GoOutLevel(string currentPath) => currentPath.Replace($"/{currentPath.Split("/")}", "");
    }
}
