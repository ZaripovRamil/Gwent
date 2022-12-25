using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Dtos;
using Models.Dtos.GameStartRequest;
using Models.Dtos.GameStartResponse;
using Models.Dtos.MoveResult;

namespace GwentClient;

public class GameRunner
{
    private Client Client { get; }
    public int ThisPlayerId { get; set; }
    private string? Player1Name { get; set; }
    private string? Player2Name { get; set; }
    private Game? Game { get; set; }
    
    public bool IsFilled => Game is not null;

    internal Queue<MoveResult> ReceivingMovesQueue { get; }
    
    //TODO Add moves here from UI
    internal Queue<PlayerMove> SendingMovesQueue { get; }

    public GameRunner(Client client)
    {
        Client = client;
        ReceivingMovesQueue = new Queue<MoveResult>();
        SendingMovesQueue = new Queue<PlayerMove>();
    }
    //TODO Call this from UI
    public void AskForGameStart(string name)
    {
        Client.SendStartRequest(new GameStartRequest(name));
    }

    public void RunGame(GameStartResponse response)
    {
        ThisPlayerId = response.ThisPlayerNumber;
        Player1Name = response.Player1Name;
        Player2Name = response.Player2Name;
        Game = new Game(response);
        //TODO здесь отправить на отрисовку состояние игры
        Task.Run(StartGame);
    }

    private void StartGame()
    {
        if (Game is null || Player1Name is null || Player2Name is null)
            throw new Exception("GameRunner didn't run correctly");
        while (!Game.IsGameFinished)
        {
            if (SendingMovesQueue.Count != 0)
            {
                Client.SendMove(SendingMovesQueue.Dequeue());
            }
            if (ReceivingMovesQueue.Count != 0)
            {
                var game = Game.ExecuteMove(ReceivingMovesQueue.Dequeue());
                //TODO преобразовать и отправить game на отрисовку
                if (Game.IsRoundFinished)
                {
                    var roundResult = Game.CalculateRoundResult();
                    //TODO отправить на отрисовку окончание раунда
                    
                    if (Game.IsGameFinished)
                    {
                        var gameResult = Game.CalculateGameResult();
                        //TODO отправить на отрисовку окончание игры
                    }
                }

               
            }
        }
    }
}