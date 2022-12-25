namespace Models.Dtos.MoveResult;

public class MoveResult
{
    public MoveResult(Player player, int cardPositionInHand, int row, int cardPositionInRow):
        this(player.Id, false,cardPositionInHand, row, cardPositionInRow,new List<byte>())
    {
    }

    public MoveResult(Player player, bool hasPassed):
        this(player.Id, hasPassed, 0,0,0,new List<byte>())
    {
    }

    public MoveResult(int id, bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow, List<byte> pulledCards)
    {
        PlayerId = id;
        HasPassed = hasPassed;
        CardPositionInRow = cardPositionInRow;
        Row = row;
        CardPositionInHand = cardPositionInHand;
        PulledCards = pulledCards;
    }

    public int PlayerId { get; }
    public bool HasPassed { get; }
    public int CardPositionInHand { get; }
    public int Row { get; }
    public int CardPositionInRow { get; }
    public List<byte> PulledCards { get; set; }
}