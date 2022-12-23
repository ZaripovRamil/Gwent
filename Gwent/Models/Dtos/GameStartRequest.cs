using Protocol.Serializator;

namespace Models.Dtos;

public class GameStartRequest
{
    public GameStartRequest(string playerName)
    {
        PlayerName = playerName;
    }

    [XField(1)] public string PlayerName { get; }
}