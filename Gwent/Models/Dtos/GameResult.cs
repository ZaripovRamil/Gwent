namespace Models.Dtos;

public class GameResult
{
    public GameResult(bool isDraw, string? winnerName)
    {
        IsDraw = isDraw;
        WinnerName = winnerName;
    }

    public bool IsDraw { get; set; }
    public string? WinnerName { get; set; }
}