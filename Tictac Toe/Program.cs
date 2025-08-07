// Opgave
// Step 0: Find ud af, hvordan du vil repræsentere et board state. Diskutér dette med gruppemedlem og 
// Og få den godkendt af underviseren.
// Step 1: Byg en AI der kan lave et tilfældigt træk. Lad denne AI være spiller 1.
//          Vind over din tilfældige AI i et spil
//Bemærk dog at du skal finde ud af, hvad du gør, hvis din AI taber så meget, at den ikke kan gøre et træk
//Step 2: Byg en AI, der kopierer hexapawns måde at lære på.
//Sæt en høj discovery i starten og lad den spille mod en tilfældig modstander ca. 200 spil.
//Step 3: 
// Lav en AI til spiller 1 som bruger hexapawn måden at lære på og lad de to spil lære mod hinanden.
// sæt discovery til 0 og se, hvor hurtigt de konsekvent spiller lige op mod hinanden.


class Program
{
    //making array and
    //by default I am providing 0-9 where no use of zero
    static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static char[] Permanent = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int player = 1; //By default player 1 is set
    static int choice; //This holds the choice at which position user want to mark
                       // The flag variable checks who has won if it's value is 1 then someone has won the match
                       //if -1 then Match has Draw if 0 then match is still running
    static int flag = 0;
    static void Main(string[] args)
    {
        //    QLearn qol = new QLearn();
        for (int i = 0; i < 2000; i++)
        {

            arr = (char[])Permanent.Clone();


            do
            {
                Console.Clear();// whenever loop will be again start then screen will be clear
                Console.WriteLine("Player1:X and Player2:O");
                Console.WriteLine("\n");

                if (player % 2 == 0)//checking the chance of the player
                {


                    Console.WriteLine("Player 2 Turn");
                    Console.WriteLine("\n");
                    Board();// calling the board Function
                    choice = int.Parse(Console.ReadLine());//Taking users choice
                                                           // checking that position where user want to run is marked (with X or O) or not
                    if (arr[choice] != 'X' && arr[choice] != 'O')
                    {
                        if (player % 2 == 0) //if chance is of player 2 then mark O else mark X
                        {
                            arr[choice] = 'O';
                            player++;
                        }
                        else
                        {
                            arr[choice] = 'X';
                            player++;
                        }
                    }
                    else
                    //If there is any possition where user want to run
                    //and that is already marked then show message and load board again
                    {
                        Console.WriteLine("Sorry the row {0} is already marked with {1}", choice, arr[choice]);
                        Console.WriteLine("\n");
                        Console.WriteLine("Please wait 2 second board is loading again.....");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    Console.WriteLine("AI");
                    List<char> moves = arr.Where(x => x != 'X' && x != 'O' && x != '0').ToList();
                    int choice = RND.Range(0, moves.Count);
                    int ting = int.Parse(moves[choice].ToString());
                    arr[ting] = 'X';
                    player++;
                }

                flag = CheckWin();// calling of check win
            }
            while (flag != 1 && flag != -1);
            // This loop will be run until all cell of the grid is not marked
            //with X and O or some player is not win
            Console.Clear();// clearing the console
            Board();// getting filled board again
            if (flag == 1)
            // if flag value is 1 then someone has win or
            //means who played marked last time which has win
            {
                Console.WriteLine("Player {0} has won", (player % 2) + 1);
            }
            else// if flag value is -1 the match will be draw and no one is winner
            {
                Console.WriteLine("Draw");
            }
            Console.ReadLine();
        }
    }
    public static class RND
    {
        private static Random Rnd = new Random();
        public static int Range(int a, int b)
        {
            return Rnd.Next(a, b);
        }


    }


    // Board method which creats board
    private static void Board()
    {
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);
        Console.WriteLine("     |     |      ");
    }
    //Checking that any player has won or not
    private static int CheckWin()
    {
        #region Horzontal Winning Condtion
        //Winning Condition For First Row
        if (arr[1] == arr[2] && arr[2] == arr[3])
        {
            return 1;
        }
        //Winning Condition For Second Row
        else if (arr[4] == arr[5] && arr[5] == arr[6])
        {
            return 1;
        }
        //Winning Condition For Third Row
        else if (arr[7] == arr[8] && arr[8] == arr[9])
        {
            return 1;
        }
        #endregion
        #region vertical Winning Condtion
        //Winning Condition For First Column
        else if (arr[1] == arr[4] && arr[4] == arr[7])
        {
            return 1;
        }
        //Winning Condition For Second Column
        else if (arr[2] == arr[5] && arr[5] == arr[8])
        {
            return 1;
        }
        //Winning Condition For Third Column
        else if (arr[3] == arr[6] && arr[6] == arr[9])
        {
            return 1;
        }
        #endregion
        #region Diagonal Winning Condition
        else if (arr[1] == arr[5] && arr[5] == arr[9])
        {
            return 1;
        }
        else if (arr[3] == arr[5] && arr[5] == arr[7])
        {
            return 1;
        }
        #endregion
        #region Checking For Draw
        // If all the cells or values filled with X or O then any player has won the match
        else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
        {
            return -1;
        }
        #endregion
        else
        {
            return 0;
        }
    }
}