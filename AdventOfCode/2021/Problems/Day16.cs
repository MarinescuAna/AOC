using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day16 : BaseDay
    {
        private List<string> BinaryContent = new List<string>();
        private Dictionary<string, string> HexaToBynary = new Dictionary<string, string>
        {
            {"0","0000"},
            {"1","0001"},
            {"2","0010"},
            {"3","0011"},
            {"4","0100"},
            {"5","0101"},
            {"6","0110"},
            {"7","0111"},
            {"8","1000"},
            {"9","1001"},
            {"A","1010"},
            {"B","1011"},
            {"C","1100"},
            {"D","1101"},
            {"E","1110" },
            {"F","1111"}
        };
        public Day16() : base(16, "Packet Decoder")
        {
            ReadInput();
        }

        private string SelectNBites(int n, ref int startIndex)
        {
            var sequence = string.Empty;
            for (; startIndex < n; )
            {
                sequence = string.Concat(sequence, BinaryContent[startIndex++]);
            }
            return sequence;
        }
        private void DecodeLiteralValue(ref int startIndex, int lengthPacket)
        {
            var group = string.Empty;
            var prefix = ' ';
            var numberString = string.Empty;
            for (var length = 0; length < (lengthPacket == 0 ? BinaryContent.Count() : lengthPacket);)
            {
                group = SelectNBites(startIndex + 5, ref startIndex);
                Console.WriteLine("after selecting the group startIndex = " + startIndex);
                length += 5;
                prefix = group[0];
                group = group.Remove(0, 1);
                numberString = string.Concat(numberString, group);
                if (prefix == '0')
                {
                    startIndex += (lengthPacket - length);
                    Console.WriteLine("after removing the 0  startIndex = " + startIndex);
                    break;
                }
            }
            Console.WriteLine("after DecodeLiteralValue startIndex = " + startIndex);

        }

        private int TakeVersions(int startIndex, int packetLength)
        {
            if (startIndex > BinaryContent.Count()-11)
            {
                return 0;
            }
            else
            {
                Console.WriteLine("startIndex = "+ startIndex);
                var version = Convert.ToInt32(BinaryContent[startIndex++] + BinaryContent[startIndex++] + BinaryContent[startIndex++], 2);
                if (version == 0)
                {
                    while (BinaryContent[startIndex++] == "0");
                    version = Convert.ToInt32(BinaryContent[startIndex++] + BinaryContent[startIndex++] + BinaryContent[startIndex++], 2);
                    Console.WriteLine("after skiping the 0 startIndex = " + startIndex);
                }
                Console.WriteLine("after version startIndex = " + startIndex);
                Console.WriteLine(version);
                var typeID = Convert.ToInt32(BinaryContent[startIndex++] + BinaryContent[startIndex++] + BinaryContent[startIndex++], 2);
                Console.WriteLine("after typeID startIndex = " + startIndex);
                switch (typeID)
                {
                    case 4:
                        DecodeLiteralValue(ref startIndex, packetLength - 6);
                        return version;
                    default:
                        {
                            var lengthTypeID = BinaryContent[startIndex++];
                            Console.WriteLine("startIndex = " + startIndex);
                            if (lengthTypeID == "0")
                            {
                                var temp = Convert.ToInt32(SelectNBites(startIndex + 15, ref startIndex), 2);
                                Console.WriteLine("startIndex = " + startIndex);
                                return version + TakeVersions(startIndex, temp);
                            }
                            else
                            {
                                var subPacketsNumber = Convert.ToInt32(SelectNBites(startIndex + 11, ref startIndex), 2);
                                Console.WriteLine("startIndex = " + startIndex);
                                if (subPacketsNumber <= 1)
                                {
                                    return version + TakeVersions(startIndex, BinaryContent.Count() - startIndex);
                                }
                                else
                                {
                                    for (int split = 0; split < subPacketsNumber; split++)
                                    {
                                        var temp = packetLength != 0 ? packetLength / subPacketsNumber:(BinaryContent.Count() - startIndex)/subPacketsNumber;
                                        version += TakeVersions(startIndex, temp);
                                        startIndex += temp;
                                        Console.WriteLine("come back after DecodeLiteralValue startIndex = " + startIndex);
                                    }
                                    return version;

                                }
                            }
                        }
                }
            }
        }
        public override void Part1()
        {
            Display(TakeVersions(0, 0).ToString(), true);
        }

        public override void Part2()
        {

        }

        public override void ReadInput()
        {
            var binary = StreamReader.ReadLine().Select(u => HexaToBynary[u.ToString()]).ToList();
            binary.ForEach(u =>
            {
                foreach (var bit in u)
                {
                    Console.Write(bit);
                    BinaryContent.Add(bit.ToString());
                }
            });
        }
    }
}
