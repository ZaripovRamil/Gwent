using XProtocol.Serializator;

namespace Models.Dtos;

public class MoveResult
{
    public MoveResult(Player player, int cardPositionInHand, int row, int cardPositionInRow)
    {
        HasPassed = false;
        PlayerName = player.Name;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
        PulledCards = new List<int>();
    }

    public MoveResult(Player player, bool hasPassed)
    {
        PlayerName = player.Name;
        HasPassed = hasPassed;
        PulledCards = new List<int>();
    }
    [XField(1)]
    public string PlayerName { get; set; }
    [XField(2)]
    public bool HasPassed { get; set; }
    [XField(3)]
    public int CardPositionInHand { get; set; }
    [XField(4)]
    public int Row { get; set; }
    [XField(5)]
    public int CardPositionInRow { get; set; }
    [XField(6)]
    public List<int> PulledCards { get; set; }
    [XField(7)]
    public bool IsLastMoveInRound { get; set; }
}