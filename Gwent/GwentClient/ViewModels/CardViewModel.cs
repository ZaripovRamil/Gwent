using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GwentClient.Models;
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
        public string Name { get; }

        public CardViewModel(Card card)
        {
            Id = card.Id;
            currentPower = card.CurrentPower;
            Role = card.Role;
            Name = card.Name;

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            CardImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/CardImages/{Id}.png")));
            RoleImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/RoleImages/{Role}.png")));
        }
    }
}
