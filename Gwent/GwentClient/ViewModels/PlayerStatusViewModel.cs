using ReactiveUI;

namespace GwentClient.ViewModels
{
    public class PlayerStatusViewModel : ViewModelBase
    {
        private string lives;
        public string Lives
        {
            get => lives;
            set => this.RaiseAndSetIfChanged(ref lives, value + "◊");
        }

        public int CurrentLives { get; set; }

        public int sumPower;
        public int SumPower
        {
            get => sumPower;
            set => this.RaiseAndSetIfChanged(ref sumPower, value);
        }

        public string Name { get; }

        public PlayerStatusViewModel(string name, int currentLives)
        {
            Name = name;
            CurrentLives = currentLives;
            Lives = currentLives.ToString();
        }
    }
}
