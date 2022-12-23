using GwentClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GwentClient.ViewModels
{
    public class GameFieldViewModel : ViewModelBase
    {
        public string PlayerName { get; }
        public string EnemyName { get; }
        public ObservableCollection<CardViewModel> Hand { get; set; }
        public ObservableCollection<CardViewModel> PlayerShooter { get; set; }
        public ObservableCollection<CardViewModel> PlayerMelee { get; set; }
        public ObservableCollection<CardViewModel> EnemyMelee { get; set; }
        public ObservableCollection<CardViewModel> EnemyShooter { get; set; }
        public GameFieldViewModel(List<int> hand, string playerName, string enemyName)
        {
            PlayerName = playerName;
            EnemyName = enemyName;

            Hand = new ObservableCollection<CardViewModel>();
            foreach(var cardId in hand)
            {
                var card = CardLibrary.Cards[cardId];
                Hand.Add(new CardViewModel(card));
            }
        }
    }
}
