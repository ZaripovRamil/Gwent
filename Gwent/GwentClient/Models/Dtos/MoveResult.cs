using System.Collections.Generic;

namespace GwentClient.Models.Dtos
{
    public class MoveResult
    {
        public string PlayerName { get; }
        public bool HasPassed { get; }
        public int CardPositionInHand { get; }
         public int Row { get; }
        public int CardPositionInRow { get; }
        public List<int> PulledCards { get; set; }
        public bool IsLastMoveInRound { get; set; }
    }
}
