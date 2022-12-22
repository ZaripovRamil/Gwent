namespace Models.FeaturesRepo;

public static class CardLibrary
{
    private static Dictionary<int, Card> Cards { get; } =
        new()
        {
            {1, new Card(1,Role.Melee, 3,null)},
            {2, new Card(2,Role.Shooter, 2,null)}
        };

    public static Card GetCard(int id) => Cards[id].GetCopy();

    //TODO fill dictionary
}