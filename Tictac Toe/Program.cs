using System.Collections.Generic;
using System.Linq;

    namespace Tictac_Toe
    {
        public class Program
        {
            static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            static char[] Permanent = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            static int player = 1;
            static int flag = 0;

            // Memory for board states and possible moves
            static Dictionary<string, List<int>> memory = new();
            // To track moves made by AI in a game
            static List<(string board, int move)> aiMoves = new();

            static void Main(string[] args)
            {
                for (int game = 0; game < 200; game++)
                {
                    arr = (char[])Permanent.Clone();
                    player = 1;
                    flag = 0;
                    aiMoves.Clear();

                    do
                    {
                        if (player % 2 == 0)
                        {
                            // Player 2: random
                            List<char> moves = arr.Where(x => x != 'X' && x != 'O' && x != '0').ToList();
                            int choice = RND.Range(0, moves.Count);
                            int ting = int.Parse(moves[choice].ToString());
                            arr[ting] = 'O';
                            player++;
                        }
                        else
                        {
                            // Player 1: learning AI
                            string boardKey = new string(arr);
                            if (!memory.ContainsKey(boardKey))
                            {
                                // Add all possible moves for this board
                                memory[boardKey] = arr
                                    .Select((c, i) => i)
                                    .Where(i => i > 0 && arr[i] != 'X' && arr[i] != 'O')
                                    .ToList();
                            }
                            var possibleMoves = memory[boardKey];
                            int move;
                            if (possibleMoves.Count > 0)
                            {
                                // High discovery: pick random from possible moves
                                move = possibleMoves[RND.Range(0, possibleMoves.Count)];
                                arr[move] = 'X';
                                aiMoves.Add((boardKey, move));
                            }
                            player++;
                        }
                        flag = CheckWin();
                    // Print result after each game
                    Console.WriteLine($"Game {game + 1}: {(flag == 1 ? "Win" : "Draw")}");
                        
                }
                    while (flag != 1 && flag != -1);
                // Print result after each game (only once)
                Console.WriteLine($"Game {game + 1}: {(flag == 1 ? "Win" : "Draw")}");
                PrintBoard();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();

                // Update memory after game
                if (flag == 1 && (player % 2) == 0)
                    {
                        // AI lost: remove last move from memory
                        var last = aiMoves.LastOrDefault();
                        if (memory.ContainsKey(last.board))
                            memory[last.board].Remove(last.move);
                    }
                    // If AI wins, keep moves (do nothing)
                }
            }

            public static class RND
            {
                private static System.Random Rnd = new System.Random();
                public static int Range(int a, int b)
                {
                    return Rnd.Next(a, b);
                }
            }

            private static int CheckWin()
            {
                if (arr[1] == arr[2] && arr[2] == arr[3]) return 1;
                else if (arr[4] == arr[5] && arr[5] == arr[6]) return 1;
                else if (arr[7] == arr[8] && arr[8] == arr[9]) return 1;
                else if (arr[1] == arr[4] && arr[4] == arr[7]) return 1;
                else if (arr[2] == arr[5] && arr[5] == arr[8]) return 1;
                else if (arr[3] == arr[6] && arr[6] == arr[9]) return 1;
                else if (arr[1] == arr[5] && arr[5] == arr[9]) return 1;
                else if (arr[3] == arr[5] && arr[5] == arr[7]) return 1;
                else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' &&
                         arr[4] != '4' && arr[5] != '5' && arr[6] != '6' &&
                         arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
                    return -1;
                else return 0;
            }

            static void PrintBoard()
            {
                Console.WriteLine($"{arr[1]}|{arr[2]}|{arr[3]}");
                Console.WriteLine("-+-+-");
                Console.WriteLine($"{arr[4]}|{arr[5]}|{arr[6]}");
                Console.WriteLine("-+-+-");
                Console.WriteLine($"{arr[7]}|{arr[8]}|{arr[9]}");
                Console.WriteLine();
            }
        }
}

