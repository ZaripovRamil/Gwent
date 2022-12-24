namespace Models.Dtos.GameStartRequest;

public class GameStartRequest
{
    public GameStartRequest(string playerName)
    {
        PlayerName = playerName;
    }
    
    public string PlayerName { get; }
}