using System.Text.Json;

namespace Models.MoveResults;

public class MoveResult
{
    public MoveResult(Player player)
    {
        PlayerMoved = player;
        Actions = new List<IGameAction>();
    }
    
    

    public Player PlayerMoved { get; set; }
    public List<IGameAction>Actions { get; set; }
    //TODO Learn to differ JSON types for deserialization

    public byte[] GetJsons(Player player)
    {
        var actionsJsons = new List<byte>();
        foreach (var action in Actions)
            actionsJsons.AddRange(JsonSerializer.SerializeToUtf8Bytes(action));
        return JsonSerializer.SerializeToUtf8Bytes(actionsJsons.ToArray());
        //TODO make actual conversion to JSON, this won't work
    }
}