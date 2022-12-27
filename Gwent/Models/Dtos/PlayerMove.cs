using Protocol.Serializator;

namespace Models.Dtos;

public class PlayerMove
{
    public PlayerMove()
    {
    }

    public PlayerMove(int playerId, bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow)
    {
        PlayerId = playerId;
        HasPassed = hasPassed;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
    }

    [XField(1)] public int PlayerId;
    [XField(2)] public bool HasPassed;
    [XField(3)] public int CardPositionInHand;
    [XField(4)] public int Row;
    [XField(5)] public int CardPositionInRow;
}