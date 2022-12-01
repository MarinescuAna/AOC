using AdventOfCode2022;

if(DateTime.Now.Month!=12 || DateTime.Now.Year!=2022 || !Enumerable.Range(1,25).Contains(DateTime.Now.Day))
{
    Console.WriteLine("Call each class by yourself! :) ");
    return;
}

var objectType = Type.GetType($"AdventOfCode2022.Implementations.Day{DateTime.Now.Day}");

var objDay = Activator.CreateInstance(objectType) as IBase;

objDay?.Run();