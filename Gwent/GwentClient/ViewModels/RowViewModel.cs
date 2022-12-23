using GwentClient.Models;
using System.Collections.ObjectModel;

namespace GwentClient.ViewModels
{
    public class RowViewModel : ViewModelBase
    {
        public string RowImagePath { get; }
        public Role RowRole { get; }
        public ObservableCollection<CardViewModel> RowCards { get; set; }
        public bool IsAvailableToPlayer { get; }

        public RowViewModel(Role role, bool isAvailableToPlayer)
        {
            RowCards = new ObservableCollection<CardViewModel>();
            RowRole = role;
            RowImagePath = @"/Assets/" + RowRole + ".png";
            IsAvailableToPlayer = isAvailableToPlayer;
        }
    }
}
