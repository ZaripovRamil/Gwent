using Models.FeaturesRepo;

namespace Models;

public class Deck
{
    public Deck(IEnumerable<int> cardsInput, bool shuffle)
    {
        var cards = cardsInput.ToArray();
        if (!shuffle)
            Cards = cards
                .Select(CardLibrary.GetCard)
                .ToList();
        else
        {
            var rnd = new Random();
            Cards = new Card[cards.Length].ToList();
            var freeSpots = new List<int>();
            for (var i = 0; i < cards.Length; i++)
                freeSpots.Add(i);
            foreach (var card in cards)
            {
                var spot = freeSpots[rnd.Next(0, freeSpots.Count)];
                Cards[spot] = CardLibrary.GetCard(card);
                freeSpots.Remove(spot);
            }
        }
    }

    public List<Card> Cards { get; set; }
}