namespace Models.FeaturesRepo;

public class DecksLibrary
{
    public static Dictionary<int, int[]> Decks { get; }
        = new()
        {
            {1, new[] {1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2}}
        };
}