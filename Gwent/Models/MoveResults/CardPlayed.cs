namespace Models.MoveResults;

public class CardPlayed : IGameAction
{
    public CardPlayed(Card card, int positionInRow)
    {
        CardId = card.Id;
        PositionInRow = positionInRow;
    }

    public int CardId { get; set; }
    public int PositionInRow { get; set; }
    
}