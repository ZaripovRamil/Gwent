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

            //var handList = new List<CardViewModel>();
            //foreach (var card in player.Hand)
            //    handList.Add(new CardViewModel(card));
            //Hand = new ObservableCollection<CardViewModel>(handList);

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

            SelectedCard = Hand.Count - 1;
        }

        public void ShowRoundResult(RoundResult roundResult)
        {
            if (roundResult.IsLastRound)
                return;

            if (roundResult.IsDraw)
            {
                PlayerStatus.Lives -= 1;
                EnemyStatus.Lives -= 1;
                ShowDialog("Раунд закончен!", $"Раунд закончился ничьей!");
            }
            else if (roundResult.WinnerName == PlayerName)
            {
                EnemyStatus.Lives -= 1;
                ShowDialog("Раунд закончен!", $"{PlayerName}, вы победили в этом раунде!");
            }
            else
            {
                PlayerStatus.Lives -= 1;
                ShowDialog("Раунд закончен!", $"{PlayerName}, вы проиграли в этом раунде!");
            }
        }

        public void ShowGameResult(GameResult gameResult)
        {
            if (gameResult.IsDraw)
                ShowDialog("Игра окончена!", "Ничья!");
            else
                ShowDialog("Игра окончена!", $"{PlayerName} - король Гвинта!");
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
