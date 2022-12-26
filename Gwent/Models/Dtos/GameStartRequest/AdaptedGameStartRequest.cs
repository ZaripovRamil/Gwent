using Protocol.Serializator;

namespace Models.Dtos.GameStartRequest;

public class AdaptedGameStartRequest
{
    public AdaptedGameStartRequest()
    {
    }
    public AdaptedGameStartRequest(GameStartRequest request)
    {
        (NamePart1, NamePart2, NamePart3) = Adapter.Adapt(request.PlayerName);
    }

    [XField(1)] public long NamePart1;
    [XField(2)] public long NamePart2;
    [XField(3)] public long NamePart3;

    public GameStartRequest Parse()
    {
        return new GameStartRequest(Adapter.ParseString(NamePart1, NamePart2, NamePart3));
    }
}