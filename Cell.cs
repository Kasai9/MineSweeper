namespace MinesweeperGame
{
    public class Cell
    {
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMines { get; set; }

        public bool IsReward { get; set; } // Added for Milestone 2

        public Cell()
        {
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;
            AdjacentMines = 0;
            IsReward = false;
        }

        public override string ToString()
        {
            if (!IsRevealed)
                return IsFlagged ? "F" : "#";
            else if (IsMine)
                return "*";
            else if (IsReward)
                return "R"; // Optional: show 'R' for reward cell (can be changed later)
            else
                return AdjacentMines > 0 ? AdjacentMines.ToString() : " ";
        }
    }
}
