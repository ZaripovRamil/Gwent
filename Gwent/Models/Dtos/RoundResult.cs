using Protocol.Serializator;

namespace Models.Dtos;

public class RoundResult
{
    public RoundResult(bool isDraw, string? winnerName)
    {
        IsDraw = isDraw;
        WinnerName = winnerName;
    }
    [XField(1)]
    public bool IsLastRound { get; set; }
    [XField(2)]
    public bool IsDraw { get; set; }
    [XField(3)]
    public string? WinnerName { get; set; }
}