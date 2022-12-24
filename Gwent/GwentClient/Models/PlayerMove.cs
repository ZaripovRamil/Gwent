namespace GwentClient.Models
{
    public class PlayerMove
    {
        public PlayerMove(string playerName, bool hasPassed, int cardPositionInHand, int row, int cardPositionInRow)
        {
            PlayerName = playerName;
            HasPassed = hasPassed;
            CardPositionInHand = cardPositionInHand;
            Row = row;
            CardPositionInRow = cardPositionInRow;
        }

        public string PlayerName { get; }

        public bool HasPassed { get; }

        public int CardPositionInHand { get; }

        public int Row { get; }

        public int CardPositionInRow { get; }
    }
}
