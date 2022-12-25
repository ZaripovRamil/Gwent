using Protocol.Serializator;

namespace Models.Dtos;

public class PlayerMove
{
    public PlayerMove(int playerId, bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow)
    {
        PlayerId = playerId;
        HasPassed = hasPassed;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
    }

    [XField(1)] public int PlayerId { get; }
    [XField(2)] public bool HasPassed { get; }
    [XField(3)] public int CardPositionInHand { get; }
    [XField(4)] public int Row { get; }
    [XField(5)] public int CardPositionInRow { get; }
}