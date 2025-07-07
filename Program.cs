using System;

namespace MinesweeperGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Minesweeper!");
            System.Threading.Thread.Sleep(2000);

            Board board = new Board(8, 8, 10);

            string gameState = "ongoing";

            while (gameState == "ongoing")
            {
                Console.Clear();
                board.PrintBoard();

                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1 - Visit a cell");
                Console.WriteLine("2 - Flag or unflag a cell");
                Console.WriteLine("3 - Use reward");

                int row = -1;
                int col = -1;
                string choice = "";

                try
                {
                    Console.Write("Enter your choice (1–3): ");
                    choice = Console.ReadLine();

                    Console.Write("Enter row: ");
                    row = int.Parse(Console.ReadLine());

                    Console.Write("Enter column: ");
                    col = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input. Please enter whole numbers only.");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nUnexpected error: {ex.Message}");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        board.RevealCell(row, col);
                        break;
                    case "2":
                        board.ToggleFlag(row, col);
                        break;
                    case "3":
                        board.ActivateReward(row, col);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        break;
                }

                gameState = board.DetermineGameState();
            }

            Console.Clear();
            board.PrintBoard(true);

            if (gameState == "won")
                Console.WriteLine("\nYou won!");
            else if (gameState == "lost")
                Console.WriteLine("\nYou hit a mine. Game over.");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
