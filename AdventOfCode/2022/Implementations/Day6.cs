namespace AdventOfCode2022.Implementations
{
    internal class Day6 : Base, IBase
    {
        private string _datastreamBuffer = "";
        public void Part1()
        {
            Display(resultPart1: DetectMarkers(4).ToString());
        }
        public void Part2()
        {
            Display(resultPart1: DetectMarkers(14).ToString());
        }
        private int DetectMarkers(int length)
        {
            var startOfPacketMarker = _datastreamBuffer.Substring(0, length);
            var i = 1;

            while (!IsUnique(startOfPacketMarker, length))
            {
                startOfPacketMarker = _datastreamBuffer.Substring(i, length);
                i++;
            }
            return i + length - 1;
        }
        private bool IsUnique(string substring, int length) => substring.Distinct().Count() == length;

        public void ReadData()
        {
            _datastreamBuffer = streamReader.ReadToEnd();
        }

        public void Run()
        {
            ReadData();
            Part1();
            Part2();
        }
    }
}
