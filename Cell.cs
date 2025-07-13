namespace MinesweeperGame
{
    public class Cell
    {
        public bool IsMine { get; set; } = false;
        public bool IsRevealed { get; set; } = false;
        public bool IsFlagged { get; set; } = false;
        public bool IsReward { get; set; } = false;
        public int AdjacentMines { get; set; } = 0;

        public override string ToString()
        {
            if (!IsRevealed)
                return IsFlagged ? "[F]" : "[#]";

            if (IsMine)
                return "[*]";

            if (IsReward)
                return "[$]";

            return AdjacentMines > 0 ? $"[{AdjacentMines}]" : "[ ]";
        }
    }
}
