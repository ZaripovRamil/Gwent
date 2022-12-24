using GwentClient.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace GwentClient.ViewModels
{
    public class GameFieldViewModel : ViewModelBase
    {
        public string PlayerName { get; }
        public string EnemyName { get; }

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

        public GameFieldViewModel(List<int> hand, string playerName, string enemyName)
        {
            PlayerName = playerName;
            EnemyName = enemyName;

            PlayerShooter = new RowViewModel(Role.Shooter, true);
            PlayerMelee = new RowViewModel(Role.Melee, true);
            EnemyMelee = new RowViewModel(Role.Melee, false);
            EnemyShooter = new RowViewModel(Role.Shooter, false);

            PlayerStatus = new PlayerStatusViewModel(PlayerName, 2);
            EnemyStatus = new PlayerStatusViewModel(EnemyName, 2);

            Hand = new ObservableCollection<CardViewModel>();
            foreach(var cardId in hand)
            {
                var card = CardLibrary.Cards[cardId];
                Hand.Add(new CardViewModel(card));
            }
        }
    }
}
