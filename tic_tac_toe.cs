class Program
    {
        const int ROWS = 3;
        const int COLS = 3;
        static List<int> locations = new List<int>();
        static int[,] board = new int[ROWS, COLS];

        static int checkvalue_in_loc(int loc)
        {
            int a = loc / 3;
            int b = loc % 3;
            return board[a, b];
        }

        static void putvalue_in_loc(int loc, int playedValue)
        {
            int a = loc / 3;
            int b = loc % 3;
            board[a, b] = playedValue;
        }
        
        static bool checkforWin(int placedLoc, int playedValue)
        {
            bool winFlag = false;
            switch (placedLoc)
            {
                case 0:
                    if((checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(1) == playedValue && 
                       checkvalue_in_loc(2) == playedValue) || (checkvalue_in_loc(0) == playedValue && 
                       checkvalue_in_loc(3) == playedValue && checkvalue_in_loc(6) == playedValue) ||
                       (checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(4) == playedValue && 
                        checkvalue_in_loc(8) == playedValue))
                            winFlag = true;
                    break;
                case 1:
                    if ((checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(1) == playedValue &&
                       checkvalue_in_loc(2) == playedValue) || (checkvalue_in_loc(1) == playedValue &&
                       checkvalue_in_loc(4) == playedValue && checkvalue_in_loc(7) == playedValue))
                        winFlag = true;
                    break;
                case 2:
                    if ((checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(1) == playedValue &&
                       checkvalue_in_loc(2) == playedValue) || (checkvalue_in_loc(2) == playedValue &&
                       checkvalue_in_loc(5) == playedValue && checkvalue_in_loc(8) == playedValue) ||
                       (checkvalue_in_loc(2) == playedValue && checkvalue_in_loc(4) == playedValue &&
                        checkvalue_in_loc(6) == playedValue))
                        winFlag = true;
                    break;
                case 3:
                    if ((checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(3) == playedValue &&
                       checkvalue_in_loc(6) == playedValue) || (checkvalue_in_loc(3) == playedValue &&
                       checkvalue_in_loc(4) == playedValue && checkvalue_in_loc(5) == playedValue))
                        winFlag = true;
                    break;
                case 4:
                    if ((checkvalue_in_loc(3) == playedValue && checkvalue_in_loc(4) == playedValue &&
                       checkvalue_in_loc(5) == playedValue) || (checkvalue_in_loc(1) == playedValue &&
                       checkvalue_in_loc(4) == playedValue && checkvalue_in_loc(7) == playedValue) ||
                       (checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(4) == playedValue &&
                        checkvalue_in_loc(8) == playedValue))
                        winFlag = true;
                    break;
                case 5:
                    if ((checkvalue_in_loc(3) == playedValue && checkvalue_in_loc(4) == playedValue &&
                       checkvalue_in_loc(5) == playedValue) || (checkvalue_in_loc(2) == playedValue &&
                       checkvalue_in_loc(5) == playedValue && checkvalue_in_loc(8) == playedValue))
                        winFlag = true;
                    break;
                case 6:
                    if ((checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(3) == playedValue &&
                       checkvalue_in_loc(6) == playedValue) || (checkvalue_in_loc(6) == playedValue &&
                       checkvalue_in_loc(7) == playedValue && checkvalue_in_loc(8) == playedValue) ||
                       (checkvalue_in_loc(2) == playedValue && checkvalue_in_loc(4) == playedValue &&
                        checkvalue_in_loc(6) == playedValue))
                        winFlag = true;
                    break;
                case 7:
                    if ((checkvalue_in_loc(1) == playedValue && checkvalue_in_loc(4) == playedValue &&
                       checkvalue_in_loc(7) == playedValue) || (checkvalue_in_loc(6) == playedValue &&
                       checkvalue_in_loc(7) == playedValue && checkvalue_in_loc(8) == playedValue))
                        winFlag = true;
                    break;
                case 8:
                    if ((checkvalue_in_loc(2) == playedValue && checkvalue_in_loc(5) == playedValue &&
                       checkvalue_in_loc(8) == playedValue) || (checkvalue_in_loc(6) == playedValue &&
                       checkvalue_in_loc(7) == playedValue && checkvalue_in_loc(8) == playedValue) ||
                       (checkvalue_in_loc(0) == playedValue && checkvalue_in_loc(4) == playedValue &&
                        checkvalue_in_loc(8) == playedValue))
                        winFlag = true;
                    break;
            }
            return winFlag;
        }

        static void displayGameBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    char toDisplay = (board[i, j] == 1) ? 'O' : (board[i, j] == 2) ? 'X' : '_';
                    Console.Write(toDisplay + "\t");
                }
                Console.WriteLine();
            }
        }

        /*this method becomes useful if/when the number of game trials is more than 1*/
        static void reset()
        {
            for (int i = 0; i <= 8; i++) // 0-8 represent the 9 board slots
                locations.Add(i);
            
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    board[i, j] = 0;
                }
            }
        }

        static void Main(string[] args)
        {
            bool play = true;
            Random rnd = new Random();
            int next = rnd.Next(2) + 1;

            /*fill the locations array*/
            for (int i = 0; i < 9; i++)
                locations.Add(i);

            int where_to_play = locations[rnd.Next(locations.Count)];
            int num_of_rounds = 0; // sum of the number of times both players have made moves
            while(play)
            {
                /*the next player makes a move in a random location*/
                putvalue_in_loc(loc: where_to_play, playedValue: next);
                num_of_rounds++; // increase the total number of moves made
                locations.Remove(where_to_play); // make the location unavailable
                displayGameBoard(); // display the game board

                if (num_of_rounds >= 5)
                {
                    if (checkforWin(where_to_play, next))
                    {
                        char toDisplay = (next == 1) ? 'O' : 'X';
                        Console.WriteLine();
                        Console.WriteLine(toDisplay + " wins!");
                        Console.WriteLine("Game Over...");
                        break;
                    }
                    else if (num_of_rounds == 9)
                    {
                        Console.WriteLine("There is a tie...");
                        Console.WriteLine("Game Over...");
                        break; // break out of the loop
                    }
                }

                Console.Write("Press q to quit or any other key to continue: ");
                char key = Console.ReadLine()[0];
                if (key.Equals('q'))
                    break;
                else
                {
                    next = next == 1 ? 2 : 1;
                    where_to_play = locations[rnd.Next(locations.Count)];
                }
            }
            Console.WriteLine("Simulation ends...");
            Console.ReadKey();
        }
    }
