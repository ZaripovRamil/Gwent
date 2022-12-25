using Models;
using Models.Dtos;
using ReactiveUI;
using System;

namespace GwentClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public Client Client;

        public MainWindowViewModel()
        {
            var client = new Client();
            client.Connect("127.0.0.1", 4910);

            Content = new LoginViewModel(this);
        }

        public void CreateGameField(Game game) => Content = new GameFieldViewModel(game);

        public void UpdateGameField(Game game)
        {
            var gameField = content as GameFieldViewModel;
            gameField.Update(game);
        }

        public void ShowRoundResult(RoundResult roundResult)
        {
            var gameField = content as GameFieldViewModel;
            gameField.ShowRoundResult(roundResult);
        }

        public void ShowGameResult(GameResult gameResult)
        {
            var gameField = content as GameFieldViewModel;
            gameField.ShowGameResult(gameResult);
        }
    }
}