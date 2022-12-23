using Protocol.Serializator;

namespace Models.Dtos;

public class PlayerMove
{
    public PlayerMove(string playerName, bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow)
    {
        PlayerName = playerName;
        HasPassed = hasPassed;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
    }

    [XField(1)] public string PlayerName { get; }
    [XField(2)] public bool HasPassed { get; }
    [XField(3)] public int CardPositionInHand { get; }
    [XField(4)] public int Row { get; }
    [XField(5)] public int CardPositionInRow { get; }
}