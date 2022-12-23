using Models;
using Models.Dtos;

namespace Server;

public class GameRunner
{
    private Server Server { get; }
    private string? Player1Name { get; set; }
    private string? Player2Name { get; set; }
    private Game? Game { get; set; }
    public bool IsFilled => Game is not null;

    internal Queue<PlayerMove> MovesQueue { get; }
    public GameRunner(Server server)
    {
        Server = server;
        MovesQueue = new Queue<PlayerMove>();
    }
    
    public void AddClient(string name)
    {
        if (Player1Name is null)
            Player1Name = name;
        else if (Player2Name is null)
        {
            Player2Name = name;
            Game = new Game(Player1Name, Player2Name);
            Parallel.Invoke(StartGame);
        }
        else throw new Exception("Can't add client into a filled lobby");
    }

    private void StartGame()
    {
        if (Game is null || Player1Name is null || Player2Name is null)
            throw new Exception("GameRunner didn't run correctly");
        var startResponses = Game.ProvideGameStartResponses();
        Server.SendStartResponse(Player1Name,startResponses[0]);
        Server.SendStartResponse(Player2Name, startResponses[1]);
        while (!Game.IsGameFinished)
        {
            if (MovesQueue.Count != 0)
            {
                var moveResults = Game.ExecuteMove(MovesQueue.Dequeue());
                Server.SendMoveResult(Player1Name, moveResults[0]);
                Server.SendMoveResult(Player2Name, moveResults[1]);
            }

            Thread.Sleep(100);
        }
    }
}