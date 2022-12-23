using Protocol.Serializator;

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

    [XField(1)] public string PlayerName { get; }
    [XField(2)] public bool HasPassed { get; }
    [XField(3)] public int CardPositionInHand { get; }
    [XField(4)] public int Row { get; }
    [XField(5)] public int CardPositionInRow { get; }
    [XField(6)] public List<int> PulledCards { get; set; }
    [XField(7)] public bool IsLastMoveInRound { get; set; }
}