using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GwentClient.Models;
using System;
using System.Collections.ObjectModel;

namespace GwentClient.ViewModels
{
    public class RowViewModel : ViewModelBase
    {
        public Bitmap RowImage { get; }
        public Role RowRole { get; }
        public ObservableCollection<CardViewModel> RowCards { get; set; }
        public bool IsAvailableToPlayer { get; }

        public RowViewModel(Role role, bool isAvailableToPlayer)
        {
            RowCards = new ObservableCollection<CardViewModel>();
            RowRole = role;
            IsAvailableToPlayer = isAvailableToPlayer;

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            RowImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/{RowRole}.png")));
        }

        public void AddCard()
        {

        }
    }
}
