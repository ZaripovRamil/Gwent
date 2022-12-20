namespace Models;

public abstract class PowerImpact
{
    protected PowerImpact(Func<int, int> impact, string name)
    {
        Impact = impact;
        Name = name;
    }

    public string Name { get; set; }
    public Func<int, int> Impact;
}