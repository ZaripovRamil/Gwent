namespace Models.FeaturesRepo;

public static class CardLibrary
{
    private static List<Card?> Cards { get; } =
        new()
        {
            new Card(0, Role.Melee, 0, GameImpactLibrary.GameImpacts[0]),//placeholder
            new Card(1,Role.Melee, 6,GameImpactLibrary.GameImpacts[0]),
            new Card(2,Role.Shooter, 3,GameImpactLibrary.GameImpacts[0]),
            new Card(3,Role.Melee, 3,GameImpactLibrary.GameImpacts[1]),
            new Card(4,Role.Shooter, 1,GameImpactLibrary.GameImpacts[2]),
            new Card(5,Role.Melee, 3,GameImpactLibrary.GameImpacts[4]),
            new Card(6,Role.Shooter, 2,GameImpactLibrary.GameImpacts[3])
        };

    public static List<string> CardsDesc { get; } = new()
    {
        "placeholder",
        "Strong basic melee card",
        "Basic shooter card",
        "After you play this card, you pull a card from your deck",
        "After you play this card, all your units get +1 power",
        "After you play this card, all your shooters gets +1 power",
        "After you play this card, all your melee get +1 power"
    };

    public static Card GetCard(byte id) => Cards[id]!.GetCopy();
}