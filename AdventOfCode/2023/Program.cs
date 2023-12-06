
if (DateTime.Now.Month != 12 || DateTime.Now.Year != 2023 || !Enumerable.Range(1, 25).Contains(DateTime.Now.Day))
{
    Console.WriteLine("Call each class by yourself! :) ");
    return;
}

var objectType = Type.GetType($"AdventOfCode2023.Day{DateTime.Now.Day}.Solution");

var objDay = Activator.CreateInstance(objectType) as AdventOfCode2023.Base;

objDay?.Run();