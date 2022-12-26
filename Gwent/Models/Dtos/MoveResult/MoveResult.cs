namespace Models.Dtos.MoveResult;

public class MoveResult
{
    public MoveResult(){}
    
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

    public int PlayerId;
    public bool HasPassed;
    public int CardPositionInHand;
    public int Row;
    public int CardPositionInRow;
    public List<byte> PulledCards;
}