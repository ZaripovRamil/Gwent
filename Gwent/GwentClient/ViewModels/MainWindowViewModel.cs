using Models;
using Models.Dtos;
using ReactiveUI;

namespace GwentClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        public GameRunner GameRunner;

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
            GameRunner = new GameRunner(client, this);
            client.GameRunner = GameRunner;
            Client = client;

            CreateLogin();
        }

        public void CreateLogin() => Content = new LoginViewModel(this);

        public void CreateGameField(Game game, int thisPlayerNumber)
            => Content = new GameFieldViewModel(game, GameRunner, thisPlayerNumber);

        public void UpdateGameField(Game game)
        {
            var gameField = content as GameFieldViewModel;
            if (gameField == null)
                return;
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