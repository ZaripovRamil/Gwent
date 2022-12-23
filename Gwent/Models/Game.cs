using Models.Dtos;

namespace Models;

public class Game //TODO Start game here
{
    public readonly Player[] Players;
    public Game(string player1Name, string player2Name) //Server creates game here 
    {
        var player1 = new Player(player1Name, this, 1);
        var player2 = new Player(player2Name, this, 1);
        Players = new[] {player1, player2};
        CurrentlyMoving = player1;
    }

    public Game(GameStartResponse startInfo) //Client creates game here
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

        Players = new[] {player1, player2};
        CurrentlyMoving = player1;
    }

    public Dictionary<int, GameStartResponse> ProvideGameStartResponses()
    {
        var res = new Dictionary<int, GameStartResponse>();
        res.Add(0, new GameStartResponse(Players[0].Name, Players[1].Name,
            0, Players[0].Hand.Select(card => card.Id).ToArray()));
        res.Add(1, new GameStartResponse(Players[0].Name, Players[1].Name,
            1, Players[1].Hand.Select(card => card.Id).ToArray()));
        return res;
    }


    public Player CurrentlyMoving { get; set; }
    public bool IsRoundFinished => Players.All(p => p.HasPassed = true);
    public bool IsGameFinished => Players.Any(player => player.Lives == 0);

    public void SetupNextRound()
    {
        foreach (var player in Players)
            player.OwnField = Player.SetupField();
    }

    public MoveResult ExecuteMove(PlayerMove move)
    {
        if (CurrentlyMoving.Name != move.PlayerName) return null;
        var moveResult = move.HasPassed
            ? CurrentlyMoving.Pass()
            : CurrentlyMoving.PlayCard(move.CardPositionInHand, move.Row, move.CardPositionInRow);
        if (IsRoundFinished) moveResult.IsLastMoveInRound = true;
        else CurrentlyMoving = CalculateNextMovingPlayer();
        return moveResult;
    }

    private Player CalculateNextMovingPlayer()
    {
        if (Players.All(player => player.HasPassed)) throw new Exception("There is no next player in a finished round");
        var otherPlayer = Players.FirstOrDefault(player => player != CurrentlyMoving && !player.HasPassed);
        return otherPlayer ?? CurrentlyMoving;
    }

    public RoundResult CalculateRoundResult()
    {
        var player1 = Players[0];
        var player2 = Players[1];
        RoundResult result;
        if (player1.Power > player2.Power)
        {
            result = new RoundResult(false, player1.Name);
            player2.Lives -= 1;
        }
        else if (player1.Power < player2.Power)
        {
            result = new RoundResult(false, player1.Name);
            player1.Lives -= 1;
        }
        else
        {
            result = new RoundResult(true, null);
            player1.Lives -= 1;
            player2.Lives -= 1;
        }

        if (Players.Any(player => player.Lives == 0))
            result.IsLastRound = true;
        else
            SetupNextRound();
        return result;
    }

    public GameResult CalculateGameResult()
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