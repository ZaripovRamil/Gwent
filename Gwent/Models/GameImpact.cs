using Models.Dtos;

namespace Models;

public abstract class GameImpact//this class is responsible for card effects(pull card/buff someone/etc)
{
    protected GameImpact(Action<GameField, Player, MoveResult> impact, TriggerType triggerType, string name)
    {
        Impact = impact;
        TriggerType = triggerType;
        Name = name;
    }

    public TriggerType TriggerType { get; set; }
    public string Name { get; set; }
    public Action<GameField, Player, MoveResult> Impact;
}