using Models.Dtos;

namespace Models;

public class Player
{
    public Player(string name)
    {
        Name = name;
        OwnField = new List<Row> {new(Role.Melee), new(Role.Shooter)};
    }

    public int Lives { get; set; }

    public int Power
    {
        get { return OwnField.Sum(row => row.Power); }
    }

    public GameField FullField { get; set; }
    public List<Row> OwnField { get; }
    public string Name { get; set; }
    public List<Card> Hand { get; set; }
    public List<Card> Deck { get; set; }

    public MoveResult PlayCard(int positionInHand, int rowIndex, int positionInRow)
    {
        if (positionInHand >= Hand.Count || positionInHand < 0) throw new IndexOutOfRangeException("No card in hand");
        var card = Hand[positionInHand];
        if (rowIndex != (int) card.Role) throw new ArgumentException("Wrong row");
        var rowCards = OwnField[rowIndex].Cards;
        if (positionInRow > rowCards.Count || positionInRow < 0)
            throw new IndexOutOfRangeException("Wrong position in a row");
        var result = new MoveResult(this, positionInHand, rowIndex, positionInRow);
        rowCards.Insert(positionInRow, card);
        if(card.OwnImpact.TriggerType == TriggerType.OnPlay)
            card.OwnImpact.Impact(FullField, this, result);
        return result;
    }

    public MoveResult Pass()
    {
        var result = new MoveResult(this, true);
        return result;
    }

    public void PullCard(MoveResult result)
    {
        if (Deck.Count == 0) return;
        var card = Deck[0];
        Hand.Add(card);
        Deck.Remove(card);
        result.PulledCards.Add(card.Id);
    }
}