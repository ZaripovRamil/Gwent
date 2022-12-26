using Protocol.Serializator;

namespace Models.Dtos.GameStartResponse;

public class AdaptedGameStartResponse
{
    public AdaptedGameStartResponse(){}
    public AdaptedGameStartResponse(GameStartResponse response)
    {
        ThisPlayerNumber = response.ThisPlayerNumber;
        (Player1NamePart1, Player1NamePart2, Player1NamePart3) = Adapter.Adapt(response.Player1Name);
        (Player2NamePart1, Player2NamePart2, Player2NamePart3) = Adapter.Adapt(response.Player2Name);
        Hand = Adapter.Adapt(response.Hand);
    }

    [XField(1)] public int ThisPlayerNumber;// 0 - player1, 1 - player2
    [XField(2)] public long Hand;
    [XField(3)] public long Player1NamePart1;
    [XField(4)] public long Player1NamePart2;
    [XField(5)] public long Player1NamePart3;
    [XField(6)] public long Player2NamePart1;
    [XField(7)] public long Player2NamePart2;
    [XField(8)] public long Player2NamePart3;
    public GameStartResponse Parse()
    {
        return new GameStartResponse(
            Adapter.ParseString(Player1NamePart1, Player1NamePart2, Player1NamePart3),
            Adapter.ParseString(Player2NamePart1, Player2NamePart2, Player2NamePart3),
            ThisPlayerNumber,
            Adapter.ParseCards(Hand).ToArray()
        );
    }
}