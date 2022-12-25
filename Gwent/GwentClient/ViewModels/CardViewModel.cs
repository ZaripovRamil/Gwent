using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Models;
using ReactiveUI;
using System;

namespace GwentClient.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private int currentPower;
        public int CurrentPower
        {
            get => currentPower;
            set => this.RaiseAndSetIfChanged(ref currentPower, value);
        }

        public int Id { get; }
        public Bitmap CardImage { get; }
        public Role Role { get; }
        public Bitmap RoleImage { get; }

        public CardViewModel(Card card)
        {
            Id = card.Id;
            currentPower = card.ResultPower;
            Role = card.Role;

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            CardImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/CardImages/{Id}.jpg")));
            RoleImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/RoleImages/{Role}.png")));
        }
    }
}
