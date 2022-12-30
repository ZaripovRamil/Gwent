using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Models;
using Models.FeaturesRepo;
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

        private IBrush textColor;
        public IBrush TextColor
        {
            get => textColor;
            set => this.RaiseAndSetIfChanged(ref textColor, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
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

            Description = CardLibrary.CardsDesc[Id];

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            CardImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/CardImages/{Id}.jpg")));
            RoleImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/RoleImages/{Role}.png")));
            ChangeColor(card);
        }

        public void ChangeColor(Card card)
        {
            if (card.BasePower == card.ResultPower)
                TextColor = Brushes.White;
            else
                TextColor = Brushes.Gold;
        }
    }
}
