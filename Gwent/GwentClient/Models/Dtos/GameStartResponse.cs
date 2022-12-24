namespace GwentClient.Models.Dtos
{
    public class GameStartResponse
    {
        public GameStartResponse(string player1Name, string player2Name, int thisPlayerNumber, int[] hand)
        {
            ThisPlayerNumber = thisPlayerNumber;
            Player1Name = player1Name;
            Player2Name = player2Name;
            Hand = hand;
        }

        public int ThisPlayerNumber { get; set; } // 0 - player1, 1 - player2

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public int[] Hand { get; set; }
    }
}
