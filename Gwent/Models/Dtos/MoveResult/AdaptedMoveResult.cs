using Protocol.Serializator;

namespace Models.Dtos.MoveResult;

public class AdaptedMoveResult
{
    public AdaptedMoveResult(MoveResult moveResult)
    {
        throw new NotImplementedException();
    }
    [XField(1)] public int PlayerId { get; }
    [XField(2)] public bool HasPassed { get; }
    [XField(3)] public int CardPositionInHand { get; }
    [XField(4)] public int Row { get; }
    [XField(5)] public int CardPositionInRow { get; }
    [XField(6)] public long PulledCards { get; }
    [XField(7)] public bool IsLastMoveInRound { get; }
    
    public MoveResult Parse()
    {
        throw new NotImplementedException();
    }
}