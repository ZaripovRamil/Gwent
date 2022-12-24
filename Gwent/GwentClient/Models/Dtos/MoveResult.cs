using System.Collections.Generic;

namespace GwentClient.Models.Dtos
{
    public class MoveResult
    {
        //public MoveResult(Player player, int cardPositionInHand, int row, int cardPositionInRow)
        //{
        //    HasPassed = false;
        //    PlayerName = player.Name;
        //    CardPositionInHand = cardPositionInHand;
        //    Row = row;
        //    CardPositionInRow = cardPositionInRow;
        //    PulledCards = new List<int>();
        //}

        //public MoveResult(Player player, bool hasPassed)
        //{
        //    PlayerName = player.Name;
        //    HasPassed = hasPassed;
        //    PulledCards = new List<int>();
        //}

        public string PlayerName { get; }
        public bool HasPassed { get; }
        public int CardPositionInHand { get; }
         public int Row { get; }
        public int CardPositionInRow { get; }
        public List<int> PulledCards { get; set; }
        public bool IsLastMoveInRound { get; set; }
    }
}
