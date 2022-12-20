namespace Models.FeaturesRepo;

public static class CardLibrary
{
    public static Dictionary<int, Card> Cards { get; set; }

    public static Card GetCard(int id) => Cards[id].GetCopy();

    //TODO fill dictionary
}