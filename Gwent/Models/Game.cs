using Models.Dtos;

namespace Models;

public class Game //TODO Start game here
{
    public int HasPassed { get; set; }
    public readonly Player[] Players;

    public Game(string player1Name, string player2Name)//Server creates game here 
    {
        var player1 = new Player(player1Name, this,1);
        var player2 = new Player(player2Name, this,1);
        Players = new[] {player1, player2};
        CurrentlyMoving = player1;
    }
    
    public Game(GameStartResponse startInfo)//Client creates game here
    {
        Player player1;
        Player player2;
        if (startInfo.ThisPlayerNumber == 0)
        {
            player1 = new Player(startInfo.Player1Name, this, startInfo.Hand);
            player2 = new Player(startInfo.Player2Name, this);
        }
        else
        {
            player2 = new Player(startInfo.Player2Name, this, startInfo.Hand);
            player1 = new Player(startInfo.Player1Name, this);
        }
        
        Players = new[]{player1, player2};
        CurrentlyMoving = player1;
    }

    public Dictionary<int, GameStartResponse> ProvideGameStartResponses()
    {
        var res = new Dictionary<int, GameStartResponse>();
        res.Add(0, new GameStartResponse(Players[0].Name, Players[1].Name, 
            0, Players[0].Hand.Select(card=>card.Id).ToArray()));
        res.Add(1, new GameStartResponse(Players[0].Name, Players[1].Name, 
            1, Players[1].Hand.Select(card=>card.Id).ToArray()));
        return res;
    }


    public Player CurrentlyMoving { get; set; }
    public bool IsRoundFinished => HasPassed == 2;
    public bool IsGameFinished => Players.Any(player=>player.Lives ==0);

    public void SetupClearField()
    {
        foreach (var player in Players)
        {
            player.OwnField[0] = new Row(Role.Melee);
            player.OwnField[1] = new Row(Role.Shooter);
        }
    }

    public MoveResult ExecuteMove(PlayerMove move)
    {
        if (move.HasPassed) return CurrentlyMoving.Pass();
        return CurrentlyMoving.PlayCard(move.CardPositionInHand, move.Row, move.CardPositionInRow);
    }

    public GameResult Result
    {
        get
        {
            if (!IsGameFinished) throw new Exception("Game result unknown yet");
            return Players[0].Lives switch
            {
                0 when Players[1].Lives == 0 => new GameResult(true, null),
                0 => new GameResult(false, Players[1].Name),
                _ => new GameResult(false, Players[0].Name)
            };
        }
    }
}