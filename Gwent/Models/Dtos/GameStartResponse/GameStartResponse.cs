namespace Models.Dtos.GameStartResponse;

public class GameStartResponse
{
    public GameStartResponse(string player1Name, string player2Name, int thisPlayerNumber, byte[] hand)
    {
        ThisPlayerNumber = thisPlayerNumber;
        Player1Name = player1Name;
        Player2Name = player2Name;
        Hand = hand;
    }

    public int ThisPlayerNumber { get; } // 0 - player1, 1 - player2
    public string Player1Name { get; }
    public string Player2Name { get; }
    public byte[] Hand { get; }
}