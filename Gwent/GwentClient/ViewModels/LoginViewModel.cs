using ReactiveUI;
using MessageBox.Avalonia;

namespace GwentClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public MainWindowViewModel MainWindow { get; set; }

        private string login;
        public string Login
        {
            get => login;
            set=> this.RaiseAndSetIfChanged(ref login, value);
        }

        private bool hasLogged = true;
        public bool HasLogged
        {
            get => hasLogged;
            set => this.RaiseAndSetIfChanged(ref hasLogged, value);
        }

        public void SendLogin()
        {
            HasLogged = false;

            if (Login == null || Login.Length < 3 || Login.Length > 8)
            {
                var dialog = MessageBoxManager.GetMessageBoxStandardWindow("Error!", "The login must be from 3 to 8 characters.");
                dialog.Show();
                return;
            }

            MainWindow.Client.GameRunner.AskForGameStart(Login);
        }

        public LoginViewModel(MainWindowViewModel mainWindow)
        {
            MainWindow = mainWindow;
        }
    }
}
