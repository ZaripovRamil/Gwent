using Protocol.Serializator;

namespace Models.Dtos.GameStartResponse;

public class AdaptedGameStartResponse
{
    public AdaptedGameStartResponse(GameStartResponse response)
    {
        throw new NotImplementedException();
    }
    [XField(1)] public int ThisPlayerNumber { get; } // 0 - player1, 1 - player2
    [XField(2)] public long Hand { get; }
    [XField(3)]public long Player1NamePart1 { get; }
    [XField(4)]public long Player1NamePart2 { get; }
    [XField(5)]public long Player1NamePart3 { get; }
    [XField(6)]public long Player2NamePart1 { get; }
    [XField(7)]public long Player2NamePart2 { get; }
    [XField(8)]public long Player2NamePart3 { get; }
    public GameStartResponse Parse()
    {
        throw new NotImplementedException();
    }
}