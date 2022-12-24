using System.Collections.Generic;

namespace GwentClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GameFieldViewModel GameField { get; }
        public MainWindowViewModel()
        {
            GameField = new GameFieldViewModel(new List<int>() { 1, 2, 3, 4, 5, 6 }, "Пупсич", "Папич");
        }
    }
}