namespace Models.FeaturesRepo;

public class DecksLibrary
{
    public static List<byte[]> Decks { get; }
        = new()
        {
            new byte[] {1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 5, 5, 6, 6}
        };
}