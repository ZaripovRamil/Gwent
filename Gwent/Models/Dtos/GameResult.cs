using Protocol.Serializator;

namespace Models.Dtos;

public class GameResult
{
    public GameResult(bool isDraw, string? winnerName)
    {
        IsDraw = isDraw;
        WinnerName = winnerName;
    }
    [XField(1)]
    public bool IsDraw { get; set; }
    [XField(2)]
    public string? WinnerName { get; set; }
}