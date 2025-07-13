using System;

namespace MinesweeperGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 8;
            int cols = 8;
            int mines = 10;

            Board board = new Board(rows, cols, mines);
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();
                board.PrintBoard();

                string gameState = board.DetermineGameState();
                if (gameState == "won")
                {
                    Console.WriteLine("Congratulations! You've cleared the minefield!");
                    break;
                }

                Console.WriteLine("Choose an action:");
                Console.WriteLine("1 - Visit a cell");
                Console.WriteLine("2 - Flag or unflag a cell");
                Console.WriteLine("3 - Use reward");
                Console.Write("Enter your choice (1-3): ");
                string choice = Console.ReadLine();

                int col, row;

                try
                {
                    Console.Write("Enter column: ");
                    col = int.Parse(Console.ReadLine());

                    Console.Write("Enter row: ");
                    row = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        bool hitMine = board.RevealCell(row, col);
                        if (hitMine)
                        {
                            Console.Clear();
                            board.PrintBoard(true); // Reveal all
                            Console.WriteLine("BOOM! You hit a mine. Game over.");
                            gameRunning = false;
                        }
                        break;

                    case "2":
                        board.ToggleFlag(row, col);
                        break;

                    case "3":
                        board.ActivateReward(row, col);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
