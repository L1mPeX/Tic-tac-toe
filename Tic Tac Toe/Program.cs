using System;

namespace TicTacToeGame
{
    class TicTacToe
    {
        static void Main(string[] args)
        {
            int[] playerPositions = { 0, 0 };
            char[] players = { 'X', 'O' };
            var currentPlayer = 1;
            string input = null;
            do
            {
                DisplayBoard(players, playerPositions);

                input = NextMove(players, playerPositions, currentPlayer);

                currentPlayer = (currentPlayer == 1) ? 2 : 1;

            } while (!GameOver(players, playerPositions, input));
            Console.ReadLine();
        }

        private static bool GameOver(char[] players, int[] playerPositions, string input)
        {
            var gameOver = false;
            var winner = DetermineWinner(playerPositions);
            if (winner > 0)
            {
                DisplayBoard(players, playerPositions);
                Console.Write($"\nPlayer '{players[winner - 1]}' has won!!!");
                gameOver = true;
            }
            else if (playerPositions[0] + playerPositions[1] == 511)
            {
                DisplayBoard(players, playerPositions);
                Console.Write($"\nThe Game was a tie!");
                gameOver = true;
            }
            else if (input.ToLower() == "q")
            {
                Console.Write($"\nPlayer, has quit the game");
                gameOver = true;
            }
            return gameOver;
        }

        private static string NextMove(char[] players, int[] playerPositions, int currentPlayer)
        {
            string input;
            bool validMove;
            do
            {
                Console.Write($"\nPlayer '{players[currentPlayer - 1]}' - Enter move: ");
                input = Console.ReadLine();
                validMove = ValidateAndMove(players, playerPositions, currentPlayer, input);
                Console.Clear();
            } while (!validMove);

            return input;
        }

        private static int DetermineWinner(int[] playerPositions)
        {
            var winner = 0;

            int[] winningMasks = { 7, 56, 448, 73, 146, 292, 84, 273 };

            foreach (int mask in winningMasks)
            {
                if ((mask & playerPositions[0]) == mask)
                {
                    winner = 1;
                    break;
                }
                if ((mask & playerPositions[1]) == mask)
                {
                    winner = 2;
                    break;
                }
            }
            return winner;
        }

        private static void DisplayBoard(char[] players, int[] playerPositions)
        {
            string[] borders =
            {
                "|", "|", "\n---+---+---\n", "|", "|", "\n---+---+---\n", "|", "|", ""
            };
            var border = 0;
            for (var position = 1; position <= 256; position <<= 1, border++)
            {
                char token = CalculateToken(players, playerPositions, position);
                Console.Write($" {token} {borders[border]}");
            }
        }

        private static char CalculateToken(char[] players, int[] playerPositions, int position)
        {
            char token;
            if ((position & playerPositions[0]) == position)
            {
                token = players[0];
            }
            else if ((position & playerPositions[1]) == position)
            {
                token = players[1];
            }
            else
            {
                token = ' ';
            }
            return token;
        }

        private static bool ValidateAndMove(char[] players, int[] playerPositions, int currentPlayer, string input)
        {
            var valid = false;
            switch (input)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    var shifter = int.Parse(input) - 1;
                    var position = 1 << shifter;
                    if (CalculateToken(players, playerPositions, position) != ' ')
                    {
                        Console.Write($"\nERROR: Square {input} has already been played!");
                    }
                    else
                    {
                        playerPositions[currentPlayer - 1] += position;
                        valid = true;
                    }
                    break;
                case "q":
                case "Q":
                    valid = true;
                    break;
                default:
                    Console.Write("\nERROR: Enter a value from 1-9. Type 'q' to quit");
                    break;
            }
            return valid;
        }
    }
}