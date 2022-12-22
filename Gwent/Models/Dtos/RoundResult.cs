namespace Models.Dtos;

public class RoundResult
{
    public RoundResult(bool isDraw, string? winnerName)
    {
        IsDraw = isDraw;
        WinnerName = winnerName;
    }

    public bool IsLastRound { get; set; }
    public bool IsDraw { get; set; }
    public string? WinnerName { get; set; }
}