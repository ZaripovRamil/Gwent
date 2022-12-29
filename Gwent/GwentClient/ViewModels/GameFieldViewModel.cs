using System.Collections.ObjectModel;
using ReactiveUI;
using Models;
using Models.FeaturesRepo;
using Models.Dtos;
using MessageBox.Avalonia;
using System.Collections.Generic;
using AvaloniaEdit.Utils;
using System.Linq;

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
            if (!HasPassed && Hand[SelectedCard].Role != row.RowRole)
                return;

            GameRunner.SendingMovesQueue.Enqueue(
                new PlayerMove(
                    PlayerNumber,
                    HasPassed,
                    SelectedCard,
                    (int)row.RowRole,
                    row.RowCards.Count + 1));
        }


        public void Update(Game game)
        {
            var player = game.Players[PlayerNumber];
            var enemy = game.Players[EnemyNumber];

            HasPassed = false;

            //Hand.RemoveAt(selectedCard);
            //Hand.Clear();
            //foreach (var card in player.Hand)
            //    Hand.Add(new CardViewModel(card));

            var handList = new List<CardViewModel>();
            foreach (var card in player.Hand)
                handList.Add(new CardViewModel(card));
            Hand = new ObservableCollection<CardViewModel>(handList);

            var isPlayerTurn = game.CurrentlyMoving == player;
            PlayerMelee.IsAvailableToPlayer = isPlayerTurn;
            PlayerShooter.IsAvailableToPlayer = isPlayerTurn;

            PlayerShooter.SetRow(player.OwnField[1]);
            PlayerMelee.SetRow(player.OwnField[0]);
            EnemyShooter.SetRow(enemy.OwnField[1]);
            EnemyMelee.SetRow(enemy.OwnField[0]);
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
