using System.Collections.ObjectModel;
using ReactiveUI;
using Models;
using Models.FeaturesRepo;
using Models.Dtos;
using MessageBox.Avalonia;
using System.Collections.Generic;
using AvaloniaEdit.Utils;
using System.Linq;
using Avalonia.Threading;

namespace GwentClient.ViewModels
{
    public class GameFieldViewModel : ViewModelBase
    {
        public GameRunner GameRunner { get; set; }

        public string PlayerName { get; }
        public string EnemyName { get; }
        public int PlayerNumber { get; }
        public int EnemyNumber => PlayerNumber == 0 ? 1 : 0;

        public bool HasPassed { get; set; }

        private bool isPassEnabled;
        public bool IsPassEnabled
        {
            get => isPassEnabled;
            set => this.RaiseAndSetIfChanged(ref isPassEnabled, value);
        }

        private ObservableCollection<CardViewModel> hand;
        public ObservableCollection<CardViewModel> Hand
        {
            get => hand;
            set => this.RaiseAndSetIfChanged(ref hand, value);
        }

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

        public void Pass()
        {
            HasPassed = true;
            PlayerMelee.SendPlayerMove();
        }

        public void SendPlayerMove(RowViewModel row)
        {
            if (SelectedCard == -1 && !HasPassed || !HasPassed && Hand[SelectedCard].Role != row.RowRole)
                return;

            GameRunner.SendingMovesQueue.Enqueue(
                new PlayerMove(
                    PlayerNumber,
                    HasPassed,
                    SelectedCard,
                    (int)row.RowRole,
                    row.RowCards.Count));
        }


        public void Update(Game game)
        {
            var player = game.Players[PlayerNumber];
            var enemy = game.Players[EnemyNumber];

            Hand = new ObservableCollection<CardViewModel>();
            foreach (var card in player.Hand)
                Hand.Add(new CardViewModel(card));
            
            var isPlayerTurn = game.CurrentlyMoving == player;
            PlayerMelee.IsAvailableToPlayer = isPlayerTurn;
            PlayerShooter.IsAvailableToPlayer = isPlayerTurn;

            IsPassEnabled = isPlayerTurn && !HasPassed;

            PlayerShooter.SetRow(player.OwnField[1]);
            PlayerMelee.SetRow(player.OwnField[0]);
            EnemyShooter.SetRow(enemy.OwnField[1]);
            EnemyMelee.SetRow(enemy.OwnField[0]);

            PlayerStatus.SumPower = player.Power;
            EnemyStatus.SumPower = enemy.Power;
            PlayerStatus.Lives = player.Lives;
            EnemyStatus.Lives = enemy.Lives;

            SelectedCard = Hand.Count - 1;
        }

        public void ShowRoundResult(RoundResult roundResult)
        {
            if (roundResult.IsLastRound)
                return;

            if (roundResult.IsDraw)
                ShowDialog("The round is over!", $"The round was draw!");
            else if (roundResult.WinnerName == PlayerName)
                ShowDialog("The round is over!", $"{PlayerName}, you won this round!");
            else
                ShowDialog("The round is over!", $"{PlayerName}, you lost this round!");
        }

        public void ShowGameResult(GameResult gameResult)
        {
            if (gameResult.IsDraw)
                ShowDialog("The game is over!", "Draw!");
            else
                ShowDialog("The game is over!", $"{PlayerName} - king of Gwent!");

            GameRunner.MainWindow.CreateLogin();
        }

        public void ShowDialog(string title, string message)
        {
            HasPassed = false;
            Dispatcher.UIThread.Post(() =>
            {
                var dialog = MessageBoxManager.GetMessageBoxStandardWindow(title, message);
                dialog.Show();
            });
        }

        public GameFieldViewModel(Game game, GameRunner gameRunner, int thisPlayerNumber)
        {
            PlayerNumber = thisPlayerNumber;
            GameRunner = gameRunner;

            var player = game.Players[PlayerNumber];
            var enemy = game.Players[EnemyNumber];

            PlayerName = player.Name;
            EnemyName = enemy.Name;

            var isPlayerTurn = game.CurrentlyMoving.Name == PlayerName;
            PlayerShooter = new RowViewModel(Role.Shooter, isPlayerTurn, this);
            PlayerMelee = new RowViewModel(Role.Melee, isPlayerTurn, this);
            EnemyMelee = new RowViewModel(Role.Melee, false, this);
            EnemyShooter = new RowViewModel(Role.Shooter, false, this);

            IsPassEnabled = isPlayerTurn;

            PlayerStatus = new PlayerStatusViewModel(PlayerName, player.Lives);
            EnemyStatus = new PlayerStatusViewModel(EnemyName, enemy.Lives);

            Hand = new ObservableCollection<CardViewModel>();
            foreach(var card in player.Hand)
            {
                Hand.Add(new CardViewModel(card));
            }
        }
    }
}
