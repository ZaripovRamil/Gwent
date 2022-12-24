namespace GwentClient.Models.Dtos
{
    public class GameStartRequest
    {
        public GameStartRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public string PlayerName { get; }
    }
}
