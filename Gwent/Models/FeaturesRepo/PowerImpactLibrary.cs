namespace Models.FeaturesRepo;

public class PowerImpactLibrary
{
    public static readonly List<PowerImpact> PowerImpacts = new()
    {
        new PowerImpact(n=>n+1, "Buff +1")
    };
}