using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day22Process
    {
        private static Day22 Day = new Day22();

        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }

    public class Day22
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In22.txt");
        private List<int> Player1Cards = new List<int>();
        private List<int> Player2Cards = new List<int>();
        public Day22()
        {
            ReadInput();
        }

       
        private void ReadInput()
        {
            var line = string.Empty;
            line = file.ReadLine();
            while (!string.IsNullOrEmpty(line = file.ReadLine()))
            {
                Player1Cards.Add(int.Parse(line));
            }
            line = file.ReadLine();
            while (!string.IsNullOrEmpty(line = file.ReadLine()))
            {
                Player2Cards.Add(int.Parse(line));
            }
        }
       
        public void Part1()
        {
            var player1Cards = new Queue<int>(Player1Cards);
            var player2Cards = new Queue<int>(Player2Cards);
            
            while(player1Cards.Count!=0 && player2Cards.Count != 0)
            {
                var player2= player2Cards.Dequeue();
                var player1 = player1Cards.Dequeue();
                if (player1 > player2)
                {
                    player1Cards.Enqueue(player1);
                    player1Cards.Enqueue(player2);
                }else
                {
                    player2Cards.Enqueue(player2);
                    player2Cards.Enqueue(player1);
                }
            }

            var sum = 0L;
            if (player1Cards.Count != 0)
            {
                var count = player1Cards.Count;
                while (player1Cards.Count != 0)
                {
                    sum = sum + count * player1Cards.Dequeue();
                    count--;
                }
            }

            if (player2Cards.Count != 0)
            {
                var count = player2Cards.Count;
                while (player2Cards.Count != 0)
                {
                    sum = sum + count * player2Cards.Dequeue();
                    count--;
                }
            }
            
            Console.WriteLine($"Day 22: Ticket Translation {sum} (part1)");
        }
          public void Part2()
        {
            
    
            //Console.WriteLine("Day 22: Ticket Translation {0} (part2)", prod);
        }
    }

}
