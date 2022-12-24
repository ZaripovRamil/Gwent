namespace GwentClient.Models
{
    public class Card
    {
        public int Id { get; }
        public Role Role { get; }
        public int CurrentPower { get; set; }
        public string Name { get; }
        public Card(int id, Role role, int currentPower, string name)
        {
            Id = id;
            Role = role;
            CurrentPower = currentPower;
            Name = name;
        }
    }
}
