using Protocol.Serializator;

namespace Models.Dtos.MoveResult;

public class AdaptedMoveResult
{
    public AdaptedMoveResult(MoveResult moveResult)
    {
        PlayerId = moveResult.PlayerId;
        HasPassed = moveResult.HasPassed;
        CardPositionInRow = moveResult.CardPositionInRow;
        CardPositionInHand = moveResult.CardPositionInHand;
        Row = moveResult.Row;
        PulledCards = Adapter.Adapt(moveResult.PulledCards);
    }

    [XField(1)] public int PlayerId { get; }
    [XField(2)] public bool HasPassed { get; }
    [XField(3)] public int CardPositionInHand { get; }
    [XField(4)] public int Row { get; }
    [XField(5)] public int CardPositionInRow { get; }
    [XField(6)] public long PulledCards { get; }

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