using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Models;
using Models.Dtos;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace GwentClient.ViewModels
{
    public class RowViewModel : ViewModelBase
    {
        private GameFieldViewModel GameField { get; }
        public Bitmap RowImage { get; }
        public Role RowRole { get; }
        public ObservableCollection<CardViewModel> RowCards { get; set; }
        private int RowNumber => (int)RowRole;

        private bool isAvailableToPlayer;
        public bool IsAvailableToPlayer
        {
            get => isAvailableToPlayer;
            set => this.RaiseAndSetIfChanged(ref isAvailableToPlayer, value);
        }

        public RowViewModel(Role role, bool isAvailableToPlayer, GameFieldViewModel gameField)
        {
            GameField = gameField;

            RowCards = new ObservableCollection<CardViewModel>();
            RowRole = role;
            IsAvailableToPlayer = isAvailableToPlayer;

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            RowImage = new Bitmap(assets.Open(new Uri($"avares://GwentClient/Assets/{RowRole}.png")));
        }

        public void SetRow(Row row)
        {
            RowCards.Clear();
            foreach (var card in row.Cards)
                RowCards.Add(new CardViewModel(card));
        }

        public void SentPlayedCard()
        {
            GameField.GameRunner.SendingMovesQueue.Enqueue(
                new PlayerMove(
                    GameField.PlayerNumber,
                    GameField.HasPassed,
                    GameField.SelectedCard,
                    RowNumber,
                    RowCards.Count + 1));
        }
    }
}
