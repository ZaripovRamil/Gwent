namespace Models.FeaturesRepo;

public static class CardLibrary
{
    private static List<Card?> Cards { get; } =
        new()
        {
            null,
            new Card(1,Role.Melee, 6,GameImpactLibrary.GameImpacts[0]),
            new Card(2,Role.Shooter, 3,GameImpactLibrary.GameImpacts[0]),
            new Card(3,Role.Melee, 3,GameImpactLibrary.GameImpacts[1]),
            new Card(4,Role.Shooter, 1,GameImpactLibrary.GameImpacts[2]),
            new Card(5,Role.Melee, 3,GameImpactLibrary.GameImpacts[3]),
            new Card(6,Role.Shooter, 2,GameImpactLibrary.GameImpacts[4])
        };

    public static Card GetCard(byte id) => Cards[id]!.GetCopy();
}