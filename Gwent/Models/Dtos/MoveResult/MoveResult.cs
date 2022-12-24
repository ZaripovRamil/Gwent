namespace Models.Dtos.MoveResult;

public class MoveResult
{
    public MoveResult(Player player, int cardPositionInHand, int row, int cardPositionInRow)
    {
        HasPassed = false;
        PlayerId = player.Id;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
        PulledCards = new List<byte>();
    }

    public MoveResult(Player player, bool hasPassed)
    {
        PlayerId = player.Id;
        HasPassed = hasPassed;
        PulledCards = new List<byte>();
    }

    public int PlayerId { get; }
    public bool HasPassed { get; }
    public int CardPositionInHand { get; }
    public int Row { get; }
    public int CardPositionInRow { get; }
    public List<byte> PulledCards { get; set; }
    public bool IsLastMoveInRound { get; set; }
}