using System;

namespace MinesweeperGame
{
    public class Board
    {
        public int Rows { get; }
        public int Columns { get; }
        public int MineCount { get; }
        public Cell[,] Grid { get; }

        private Random rand = new Random();

        public Board(int rows, int columns, int mineCount)
        {
            Rows = rows;
            Columns = columns;
            MineCount = mineCount;
            Grid = new Cell[Rows, Columns];

            InitializeBoard();
            PlaceMines();
            PlaceReward(); // Milestone 2
            CalculateAdjacentMines();
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Grid[r, c] = new Cell();
                }
            }
        }

        private void PlaceMines()
        {
            int placedMines = 0;
            while (placedMines < MineCount)
            {
                int r = rand.Next(Rows);
                int c = rand.Next(Columns);

                if (!Grid[r, c].IsMine && !Grid[r, c].IsReward)
                {
                    Grid[r, c].IsMine = true;
                    placedMines++;
                }
            }
        }

        private void PlaceReward()
        {
            bool placed = false;
            while (!placed)
            {
                int r = rand.Next(Rows);
                int c = rand.Next(Columns);

                if (!Grid[r, c].IsMine)
                {
                    Grid[r, c].IsReward = true;
                    placed = true;
                }
            }
        }

        private void CalculateAdjacentMines()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (!Grid[r, c].IsMine)
                    {
                        Grid[r, c].AdjacentMines = CountMinesAround(r, c);
                    }
                }
            }
        }

        private int CountMinesAround(int row, int col)
        {
            int count = 0;
            for (int r = row - 1; r <= row + 1; r++)
            {
                for (int c = col - 1; c <= col + 1; c++)
                {
                    if (r >= 0 && r < Rows && c >= 0 && c < Columns && Grid[r, c].IsMine)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void RevealCell(int row, int col)
        {
            if (IsValidCell(row, col) && !Grid[row, col].IsRevealed && !Grid[row, col].IsFlagged)
            {
                Grid[row, col].IsRevealed = true;

                // If no adjacent mines, reveal neighbors
                if (Grid[row, col].AdjacentMines == 0 && !Grid[row, col].IsMine)
                {
                    for (int r = row - 1; r <= row + 1; r++)
                    {
                        for (int c = col - 1; c <= col + 1; c++)
                        {
                            if (IsValidCell(r, c))
                            {
                                RevealCell(r, c);
                            }
                        }
                    }
                }
            }
        }

        public void ToggleFlag(int row, int col)
        {
            if (IsValidCell(row, col) && !Grid[row, col].IsRevealed)
            {
                Grid[row, col].IsFlagged = !Grid[row, col].IsFlagged;
            }
        }

        public void ActivateReward(int row, int col)
        {
            if (IsValidCell(row, col) && Grid[row, col].IsReward && !Grid[row, col].IsRevealed)
            {
                Grid[row, col].IsRevealed = true;
                for (int r = row - 1; r <= row + 1; r++)
                {
                    for (int c = col - 1; c <= col + 1; c++)
                    {
                        if (IsValidCell(r, c) && !Grid[r, c].IsMine)
                        {
                            Grid[r, c].IsRevealed = true;
                        }
                    }
                }
            }
        }

        public string DetermineGameState()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c].IsMine && Grid[r, c].IsRevealed)
                        return "lost";

                    if (!Grid[r, c].IsMine && !Grid[r, c].IsRevealed)
                        return "ongoing";
                }
            }
            return "won";
        }

        private bool IsValidCell(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        public void PrintBoard(bool revealAll = false)
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (revealAll)
                        Grid[r, c].IsRevealed = true;

                    Console.Write(Grid[r, c].ToString() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
