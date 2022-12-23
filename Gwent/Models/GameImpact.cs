using Models.Dtos;

namespace Models;

public class GameImpact//this class is responsible for card effects(pull card/buff someone/etc)
{
    public GameImpact(Action<Game, Player, MoveResult> impact, TriggerType triggerType, string name)
    {
        Impact = impact;
        TriggerType = triggerType;
        Name = name;
    }

    public TriggerType TriggerType { get; set; }
    public string Name { get; set; }
    public Action<Game, Player, MoveResult> Impact;
}