using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Problems
{
    public class Day21 : BaseDay
    {
        private int positionsPlayer1 = 0;
        private int positionsPlayer2 = 0;
        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;
        private int DieValue = 0;
        private int CountDieValue = 0;
        private Dictionary<(int p1Score, int p2Score, int p1Pos, int p2Pos, int nextDice, int diceRolls, int toMove), (long p1Wins, long p2Wins)> gameStates = new Dictionary<(int p1Score, int p2Score, int p1Pos, int p2Pos, int nextDice, int diceRolls, int toMove), (long p1Wins, long p2Wins)>();

        public Day21() : base(21, "Dirac Dice")
        {
            ReadInput();
        }
        private int Roll()
        {
            if (DieValue >= 100)
            {
                DieValue = 0;
            }

            CountDieValue++;
            return ++DieValue;
        }
        private void Move(int player)
        {
            var positions = Roll() + Roll() + Roll();
            if (player == 1)
            {
                positionsPlayer1 = (positionsPlayer1 + positions - 1) % 10 + 1;
                scorePlayer1 += positionsPlayer1;
            }
            else
            {
                positionsPlayer2 = (positionsPlayer2 + positions - 1) % 10 + 1;
                scorePlayer2 += positionsPlayer2;
            }

        }
        public override void Part1()
        {
            while (scorePlayer1 < 1000 && scorePlayer2 < 1000)
            {
                if (CountDieValue % 2 == 0)
                {
                    Move(1);
                }
                else
                {
                    Move(2);
                }
            }
            Display(((scorePlayer1 > scorePlayer2 ? scorePlayer2 : scorePlayer1) * CountDieValue).ToString(), true);
        }
        private (long player1Wins, long player2Wins) DiracGame((int player1Score, int player2Score, int player1Poz, int player2Poz, int nextDice, int diceRolls, int toMove) curState)
        {
            if (gameStates.ContainsKey(curState))
                return gameStates[curState];

            (int player1Score, int player2Score, int player1Poz, int player2Poz, int nextDice, int diceRolls, int toMove) = curState;

            if (toMove == 1)
            {
                player1Poz += nextDice;
                if (player1Poz > 10) player1Poz -= 10;

                //We've moved 3 times, time to add up the score.
                if (diceRolls == 2)
                {
                    player1Score += player1Poz;
                    //P1 has won on this move
                    if (player1Score >= 21)
                    {
                        gameStates[curState] = (1, 0);
                        return (1, 0);
                    }
                }
            }
            else
            {
                player2Poz += nextDice;
                if (player2Poz > 10) player2Poz -= 10;

                if (diceRolls == 2)
                {
                    player2Score += player2Poz;

                    //p2 has won on this move
                    if (player2Score >= 21)
                    {
                        gameStates[curState] = (0, 1);
                        return (0, 1);
                    }
                }
            }

            var winsTimes1 = (0l, 0l);
            var winsTimes2 = (0l, 0l);
            var winsTimes3 = (0l, 0l);
            if (diceRolls == 2)
            {
                toMove = toMove == 1 ? 2 : 1;
                winsTimes1 = DiracGame((player1Score, player2Score, player1Poz, player2Poz, 1, 0, toMove));
                winsTimes2 = DiracGame((player1Score, player2Score, player1Poz, player2Poz, 2, 0, toMove));
                winsTimes3 = DiracGame((player1Score, player2Score, player1Poz, player2Poz, 3, 0, toMove));
            }
            else
            {
                winsTimes1 = DiracGame((player1Score, player2Score, player1Poz, player2Poz, 1, diceRolls + 1, toMove));
                winsTimes2 = DiracGame((player1Score, player2Score, player1Poz, player2Poz, 2, diceRolls + 1, toMove));
                winsTimes3 = DiracGame((player1Score, player2Score, player1Poz, player2Poz, 3, diceRolls + 1, toMove));
            }
            gameStates[curState] = (winsTimes1.Item1 + winsTimes2.Item1 + winsTimes3.Item1, winsTimes1.Item2 + winsTimes2.Item2 + winsTimes3.Item2);

            return (winsTimes1.Item1 + winsTimes2.Item1 + winsTimes3.Item1, winsTimes1.Item2 + winsTimes2.Item2 + winsTimes3.Item2);

        }
        public override void Part2()
        {
            var (p1Wins, p2Wins) = DiracGame((0, 0, positionsPlayer1, positionsPlayer2, 0, -1, 1));
            Display((p1Wins > p2Wins ? p1Wins : p2Wins).ToString(), false);
        }

        public override void ReadInput()
        {
            positionsPlayer1 = int.Parse(StreamReader.ReadLine());
            positionsPlayer2 = int.Parse(StreamReader.ReadLine());
        }
    }
}
