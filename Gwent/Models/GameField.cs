namespace Models;

public class GameField
{
    public int HasPassed { get; set; }
    public (Player, Player) Players;
    public Player CurrentlyMoving { get; set; }
    public bool IsRoundFinished => HasPassed == 2;
    public bool IsGameFinished => Players.Item1.Lives == 0 || Players.Item2.Lives == 0;

    public GameResult Result
    {
        get
        {
            if (!IsGameFinished) throw new Exception("Game result unknown yet");
            return Players.Item1.Lives switch
            {
                0 when Players.Item2.Lives == 0 => new GameResult(true, null),
                0 => new GameResult(false, Players.Item2.Name),
                _ => new GameResult(false, Players.Item1.Name)
            };
        }
    }
}