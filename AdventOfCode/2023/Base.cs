namespace AdventOfCode2023
{
    public abstract class Base
    {
        // Don't forget to set "Copy to Output Directory : Copy always" 
        protected StreamReader streamReader = new StreamReader($@"Day{DateTime.Now.Day}\Input.txt");
        public abstract void Run();
    }
}
