using System.Collections.ObjectModel;
using ReactiveUI;
using Models;
using Models.FeaturesRepo;
using Models.Dtos;
using MessageBox.Avalonia;

namespace GwentClient.ViewModels
{
    public class GameFieldViewModel : ViewModelBase
    {
        public string PlayerName { get; }
        public string EnemyName { get; }
        private Client client;

        public ObservableCollection<CardViewModel> Hand { get; set; }
        public RowViewModel PlayerShooter { get; set; }
        public RowViewModel PlayerMelee { get; set; }
        public RowViewModel EnemyMelee { get; set; }
        public RowViewModel EnemyShooter { get; set; }

        public PlayerStatusViewModel PlayerStatus { get; set; }
        public PlayerStatusViewModel EnemyStatus { get; set; }

        private int selectedCard;
        public int SelectedCard
        {
            get => selectedCard;
            set => this.RaiseAndSetIfChanged(ref selectedCard, value);
        }

        public void Update(Game game)
        {
            var player = game.Players[0];
            var enemy = game.Players[1];

            Hand.Clear();
            foreach(var card in player.Hand)
                Hand.Add(new CardViewModel(card));

            var isPlayerTurn = game.CurrentlyMoving == player;
            PlayerMelee.IsAvailableToPlayer = isPlayerTurn;
            PlayerShooter.IsAvailableToPlayer = isPlayerTurn;

            PlayerShooter.SetRow(player.OwnField[1]);
            PlayerMelee.SetRow(player.OwnField[0]);
            EnemyShooter.SetRow(enemy.OwnField[0]);
            EnemyMelee.SetRow(enemy.OwnField[1]);

            PlayerStatus.Lives = player.Lives;
            EnemyStatus.Lives = enemy.Lives;
        }

        public void ShowRoundResult(RoundResult roundResult)
        {
            if (roundResult.IsDraw)
            {
                PlayerStatus.Lives -= 1;
                EnemyStatus.Lives -= 1;
                ShowDialog("Раунд закончен!", $"Раунд закончился ничьей!");
            }
            else if (roundResult.WinnerName == PlayerName)
            {
                EnemyStatus.Lives -= 1;
                ShowDialog("Раунд закончен!", $"Вы победили в этом раунде!");
            }
            else
            {
                PlayerStatus.Lives -= 1;
                ShowDialog("Раунд закончен!", $"Противник победил в этом раунде!");
            }
        }

        public void ShowGameResult(GameResult gameResult)
        {
            if (gameResult.IsDraw)
                ShowDialog("Игра окончена!", "Ничья!");
            else if (gameResult.WinnerName == PlayerName)
                ShowDialog("Игра окончена!", "Вы победили!");
            else
                ShowDialog("Игра окончена!", "Противник победил!");
        }

        public void ShowDialog(string title, string message)
        {
            var dialog = MessageBoxManager.GetMessageBoxStandardWindow(title, message);
            dialog.Show();
        }

        public GameFieldViewModel(Game game)
        {
            var player = game.Players[0];
            var enemy = game.Players[1];

            PlayerName = player.Name;
            EnemyName = enemy.Name;

            PlayerShooter = new RowViewModel(Role.Shooter, true);
            PlayerMelee = new RowViewModel(Role.Melee, true);
            EnemyMelee = new RowViewModel(Role.Melee, false);
            EnemyShooter = new RowViewModel(Role.Shooter, false);

            PlayerStatus = new PlayerStatusViewModel(PlayerName, player.Lives);
            EnemyStatus = new PlayerStatusViewModel(EnemyName, enemy.Lives);

            Hand = new ObservableCollection<CardViewModel>();
            foreach(var card in player.Hand)
            {
                Hand.Add(new CardViewModel(CardLibrary.GetCard(card.Id)));
            }
        }
    }
}
