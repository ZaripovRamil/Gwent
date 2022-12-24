using Protocol.Serializator;

namespace Models.Dtos.GameStartRequest;

public class AdaptedGameStartRequest
{
    public AdaptedGameStartRequest(GameStartRequest request)
    {
        throw new NotImplementedException();
    }

    [XField(1)]public long NamePart1 { get; }
    [XField(2)]public long NamePart2 { get; }
    [XField(3)]public long NamePart3 { get; }

    public GameStartRequest Parse()
    {
        throw new NotImplementedException();
    }
}