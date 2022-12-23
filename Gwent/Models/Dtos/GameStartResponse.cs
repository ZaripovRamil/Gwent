using Protocol.Serializator;

namespace Models.Dtos;

public class GameStartResponse
{
    public GameStartResponse(string player1Name, string player2Name, int thisPlayerNumber, int[] hand)
    {
        ThisPlayerNumber = thisPlayerNumber;
        Player1Name = player1Name;
        Player2Name = player2Name;
        Hand = hand;
    }

    [XField(3)] public int ThisPlayerNumber { get; } // 0 - player1, 1 - player2
    [XField(1)] public string Player1Name { get; }
    [XField(2)] public string Player2Name { get; }
    [XField(4)] public int[] Hand { get; }
}