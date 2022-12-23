namespace Models;

public class PowerImpact
{
    public PowerImpact(Func<int, int> impact, string name)
    {
        Impact = impact;
        Name = name;
    }

    public string Name { get; set; }
    public Func<int, int> Impact;
}