using System;
using System.Collections.Generic;
using System.Linq;

namespace Tictac_Toe
{
    public class Program
    {
        static char[] arr = new char[10];
        static char[] Permanent = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int flag = 0;

        static string ai1Name = "AI_Robot_Bob";     // X
        static string ai2Name = "AI_Robot_Bobete";  // O

        static Dictionary<string, List<int>> memory = new();
        static List<(string board, int move)> aiMoves = new();

        public static void Main(string[] args)
        {
            Console.WriteLine($"{ai1Name} (X) vs {ai2Name} (O)\n");

            for (int game = 0; game < 200; game++)
            {
                // Reset board and game state
                arr = (char[])Permanent.Clone();
                player = 1;
                flag = 0;
                aiMoves.Clear();

                do
                {
                    if (player % 2 == 0)
                    {
                        // Player 2 (O) - Random
                        List<char> moves = arr.Where(x => x != 'X' && x != 'O' && x != '0').ToList();
                        if (moves.Count > 0)
                        {
                            int choice = RND.Range(0, moves.Count);
                            int move = int.Parse(moves[choice].ToString());
                            arr[move] = 'O';
                            player++;
                        }
                    }
                    else
                    {
                        // Player 1 (X) - Learning AI
                        string boardKey = new string(arr);
                        if (!memory.ContainsKey(boardKey))
                        {
                            memory[boardKey] = arr
                                .Select((c, i) => i)
                                .Where(i => i > 0 && arr[i] != 'X' && arr[i] != 'O')
                                .ToList();
                        }

                        var possibleMoves = memory[boardKey];
                        if (possibleMoves.Count > 0)
                        {
                            int move = possibleMoves[RND.Range(0, possibleMoves.Count)];
                            arr[move] = 'X';
                            aiMoves.Add((boardKey, move));
                            player++;
                        }
                    }

                    flag = CheckWin();

                } while (flag == 0); // 1 = win, -1 = draw

                // Determine winner or draw
                string result;
                if (flag == 1)
                {
                    // Game won - Determine last player (the one who just moved)
                    string winner = (player % 2 == 0) ? ai1Name : ai2Name;
                    result = $"{winner} Win!";
                }
                else
                {
                    result = "Draw! No winner.";
                }

                Console.WriteLine($"Game {game + 1}: {result}");
                PrintBoard();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();

                // AI loses (Bob - X only) → punish
                if (flag == 1 && (player % 2 == 0)) // last move was by X
                {
                    var last = aiMoves.LastOrDefault();
                    if (memory.ContainsKey(last.board))
                        memory[last.board].Remove(last.move);
                }
            }
        }

        private static int CheckWin()
        {
            // Rows
            if (arr[1] == arr[2] && arr[2] == arr[3]) return 1;
            if (arr[4] == arr[5] && arr[5] == arr[6]) return 1;
            if (arr[7] == arr[8] && arr[8] == arr[9]) return 1;

            // Columns
            if (arr[1] == arr[4] && arr[4] == arr[7]) return 1;
            if (arr[2] == arr[5] && arr[5] == arr[8]) return 1;
            if (arr[3] == arr[6] && arr[6] == arr[9]) return 1;

            // Diagonals
            if (arr[1] == arr[5] && arr[5] == arr[9]) return 1;
            if (arr[3] == arr[5] && arr[5] == arr[7]) return 1;

            // Draw check: all positions filled
            if (Enumerable.Range(1, 9).All(i => arr[i] == 'X' || arr[i] == 'O'))
                return -1;

            // Game continues
            return 0;
        }

        private static void PrintBoard()
        {
            Console.WriteLine($"{arr[1]}|{arr[2]}|{arr[3]}");
            Console.WriteLine("-+-+-");
            Console.WriteLine($"{arr[4]}|{arr[5]}|{arr[6]}");
            Console.WriteLine("-+-+-");
            Console.WriteLine($"{arr[7]}|{arr[8]}|{arr[9]}");
            Console.WriteLine();
        }

        public static class RND
        {
            private static Random rnd = new Random();
            public static int Range(int a, int b)
            {
                return rnd.Next(a, b);
            }
        }
    }
}
