namespace GwentClient.Models.Dtos
{
    public class GameResult
    {
        public GameResult(bool isDraw, string? winnerName)
        {
            IsDraw = isDraw;
            WinnerName = winnerName;
        }

        public bool IsDraw { get; }
        public string? WinnerName { get; }
    }
}
