using Protocol.Serializator;

namespace Models.Dtos.MoveResult;

public class AdaptedMoveResult
{
    public AdaptedMoveResult()
    {
    }

    public AdaptedMoveResult(MoveResult moveResult)
    {
        PlayerId = moveResult.PlayerId;
        HasPassed = moveResult.HasPassed;
        CardPositionInRow = moveResult.CardPositionInRow;
        CardPositionInHand = moveResult.CardPositionInHand;
        Row = moveResult.Row;
        PulledCards = Adapter.Adapt(moveResult.PulledCards);
    }

    [XField(1)] public int PlayerId;
    [XField(2)] public bool HasPassed;
    [XField(3)] public int CardPositionInHand;
    [XField(4)] public int Row;
    [XField(5)] public int CardPositionInRow;
    [XField(6)] public long PulledCards;

    public MoveResult Parse()
    {
        return new MoveResult(
            PlayerId,
            HasPassed,
            CardPositionInHand,
            Row,
            CardPositionInRow,
            Adapter.ParseCards(PulledCards).ToList()
        );
    }
}