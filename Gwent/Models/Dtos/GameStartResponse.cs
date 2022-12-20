namespace Models.Dtos;

public class GameStartResponse
{
    public GameStartResponse(string opponentName, int[] hand)
    {
        OpponentName = opponentName;
        Hand = hand;
    }

    public string OpponentName { get; set; }
    public int[] Hand { get; set; }
}