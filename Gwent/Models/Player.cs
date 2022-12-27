using Models.Dtos;
using Models.Dtos.MoveResult;
using Models.FeaturesRepo;

namespace Models;

public class Player
{
    //this constructor for server
    public Player(string name, Game game, int deckId, int id) : this(name, game, id)
    {
        Hand = new List<Card>();
        Deck = new Deck(DecksLibrary.Decks[deckId], true);
        for (var i = 0; i < 8; i++)
            PullCard();
    }

    //this constructor for client
    public Player(string name, Game game, byte[] hand, int id) : this(name, game, id)
    {
        Hand = hand.Select(CardLibrary.GetCard).ToList();
    }

    //basic constructor
    private Player(string name, Game game, int id)
    {
        GameField = game;
        Id = id;
        Name = name;
        Lives = 2;
        OwnField = SetupField();
    }

    public int Lives { get; set; }

    public int Power
    {
        get { return OwnField.Sum(row => row.Power); }
    }
    public Game GameField { get; set; }
    public List<Row> OwnField { get; set; }
    public string Name { get; set; }
    public List<Card> Hand { get; set; }
    public Deck Deck { get; set; }
    public int Id { get; }

    public MoveResult PlayCard(int positionInHand, int rowIndex, int positionInRow)
    {
        if (positionInHand >= Hand.Count || positionInHand < 0) throw new IndexOutOfRangeException("No card in hand");
        var card = Hand[positionInHand];
        if (rowIndex != (int) card.Role) throw new ArgumentException("Wrong row");
        var rowCards = OwnField[rowIndex].Cards;
        var result = new MoveResult(this, positionInHand, rowIndex, positionInRow, card.Id);
        rowCards.Add(card);
        if (card.OwnImpact.TriggerType == TriggerType.OnPlay)
            card.OwnImpact.Impact(GameField, this, result);
        return result;
    }
    
    public void PlayCard(byte cardIdPlayed)
    {
        var result = new MoveResult(Id, cardIdPlayed);
        var card = CardLibrary.GetCard(cardIdPlayed);
        var rowIndex = (int)card.Role;
        OwnField[rowIndex].Cards.Add(card);
        if (card.OwnImpact.TriggerType == TriggerType.OnPlay)
            card.OwnImpact.Impact(GameField, this, result);
    }

    public MoveResult Pass()
    {
        var result = new MoveResult(this, true);
        GameField.HasPassed[Id] = true;
        return result;
    }

    public void PullCard(MoveResult result)
    {
        PullCard();
        result.PulledCards.Add(Hand[^1].Id);
    }

    public void PullCard()
    {
        if (Deck.Cards.Count == 0) return;
        var card = Deck.Cards[0];
        Hand.Add(card);
        Deck.Cards.Remove(card);
    }

    public static List<Row> SetupField() => new() {new Row(Role.Melee), new Row(Role.Shooter)};
}