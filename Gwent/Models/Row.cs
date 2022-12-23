namespace Models;

public class Row
{
    public Row(Role role)
    {
        Role = role;
        Cards = new List<Card>();
    }

    public int Power
    {
        get { return Cards.Sum(card => card.ResultPower); }
    }

    public List<Card> Cards { get; }

    public void ImpactOnRow(PowerImpact powerImpact)
    {
        foreach (var card in Cards)
            card.ForeignImpacts.Add(powerImpact);
    }

    public Role Role { get; }
}