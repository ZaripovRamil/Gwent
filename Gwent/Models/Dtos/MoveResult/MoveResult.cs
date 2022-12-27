namespace Models.Dtos.MoveResult;

public class MoveResult
{
    public MoveResult()
    {
    }

    public MoveResult(Player player, int cardPositionInHand, int row, int cardPositionInRow, byte cardIdPlayed) :
        this(player.Id, false, cardPositionInHand, row, cardPositionInRow, cardIdPlayed, new List<byte>())
    {
    }

    public MoveResult(int playerId, byte cardId) :
        this(playerId, false, 0, 0, 0, cardId, new List<byte>())
    {
    }

    public MoveResult(Player player, bool hasPassed) :
        this(player.Id, hasPassed, 0, 0, 0, 0, new List<byte>())
    {
    }

    public MoveResult(int id, bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow, byte cardId,
        List<byte> pulledCards)
    {
        CardIdPlayed = cardId;
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
    public byte CardIdPlayed;
    public int Row;
    public int CardPositionInRow;
    public List<byte> PulledCards;
}