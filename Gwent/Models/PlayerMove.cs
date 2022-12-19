namespace Models;

public class PlayerMove
{
    public PlayerMove(bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow)
    {
        HasPassed = hasPassed;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
    }

    public bool HasPassed { get; }
    public int CardPositionInHand { get; }
    public int Row { get; }
    public int CardPositionInRow { get; }
}