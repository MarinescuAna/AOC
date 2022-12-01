namespace AdventOfCode2022
{
    internal abstract class Base
    {
        protected StreamReader streamReader = new StreamReader($@"Inputs/Day{DateTime.Now.Day}.txt");
        protected void Display(string resultPart1 = "", string resultPart2 = "")
        {
            Console.WriteLine(string.IsNullOrEmpty(resultPart1)? $"Part2: {resultPart2}": $"Part1: {resultPart1}");
        }
    }
}
