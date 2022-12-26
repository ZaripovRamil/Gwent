namespace Models;

public class Card
{
    public Card(byte id, Role role, int basePower, GameImpact ownImpact)
    {
        Id = id;
        Role = role;
        BasePower = basePower;
        OwnImpact = ownImpact;
    }

    public byte Id { get; }
    public Role Role { get; }
    private int BasePower { get; }

    public GameImpact OwnImpact { get; }
    public List<PowerImpact> ForeignImpacts { get; } = new();

    public int ResultPower
    {
        get { return ForeignImpacts.Aggregate(BasePower, (current, impact) => impact.Impact(current)); }
    }

    public Card GetCopy()
    {
        return new Card(Id, Role, BasePower, OwnImpact);
    }
}