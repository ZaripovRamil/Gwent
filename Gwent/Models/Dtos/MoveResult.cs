namespace Models.Dtos;

public class MoveResult
{
    public MoveResult(Player player, int cardPositionInHand, int row, int cardPositionInRow)
    {
        HasPassed = false;
        PlayerName = player.Name;
        CardPositionInHand = cardPositionInHand;
        Row = row;
        CardPositionInRow = cardPositionInRow;
        PulledCards = new List<int>();
    }

    public MoveResult(Player player, bool hasPassed)
    {
        PlayerName = player.Name;
        HasPassed = hasPassed;
        PulledCards = new List<int>();
    }

    public string PlayerName { get; set; }
    public bool HasPassed { get; set; }
    public int CardPositionInHand { get; set; }
    public int Row { get; set; }
    public int CardPositionInRow { get; set; }
    public List<int> PulledCards { get; set; }
    public bool IsLastMoveInRound { get; set; }
}