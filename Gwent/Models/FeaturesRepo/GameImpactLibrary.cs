using Models.Dtos;

namespace Models.FeaturesRepo;

public static class GameImpactLibrary
{
    public static readonly List<GameImpact> GameImpacts = new()
    {
        new GameImpact(Actions[0], TriggerType.OnPlay, "None"),
        new GameImpact(Actions[1],TriggerType.OnPlay, "Pull card"),
        new GameImpact(Actions[2], TriggerType.OnPlay, "Buff all"),
        new GameImpact(Actions[3],TriggerType.OnPlay, "Buff melee"),
        new GameImpact(Actions[4], TriggerType.OnPlay, "Buff shooters")
    };
    private static readonly List<Action<Game, Player, MoveResult>> Actions = new()
    {
        (_, _, _) =>
        {
        },
        
        (_, player, moveResult) => player.PullCard(moveResult),
        
        (_, player, _) =>
        {
            foreach (var card in player.OwnField.SelectMany(row => row.Cards))
            {
                card.ForeignImpacts.Add(PowerImpactLibrary.PowerImpacts[0]);
            }
        },
        (_, player, _) =>
        {
            foreach (var card in player.OwnField[0].Cards)
            {
                card.ForeignImpacts.Add(PowerImpactLibrary.PowerImpacts[0]);
            }
        },
        (_, player, _) =>
        {
            foreach (var card in player.OwnField[1].Cards)
            {
                card.ForeignImpacts.Add(PowerImpactLibrary.PowerImpacts[0]);
            }
        }
    };
}

