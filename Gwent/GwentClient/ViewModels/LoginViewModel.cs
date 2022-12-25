using ReactiveUI;
using MessageBox.Avalonia;
using Protocol.Serializator;
using Protocol;
using Models.Dtos.GameStartRequest;

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

        public void SendLogin()
        {
            if (Login == null || Login.Length < 3 || Login.Length > 8)
            {
                var dialog = MessageBoxManager.GetMessageBoxStandardWindow("Ошибка!", "Логин должен быть от 3 до 8 символов.");
                dialog.Show();
                return;
            }

            MainWindow.Client.SendStartRequest(new GameStartRequest(Login));
        }

        public LoginViewModel(MainWindowViewModel mainWindow)
        {
            MainWindow = mainWindow;
        }
    }
}
