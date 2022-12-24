using ReactiveUI;
using MessageBox.Avalonia;
using Protocol.Serializator;
using Protocol;
using GwentClient.Models.Dtos;

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

            MainWindow.Client.QueuePacketSend(
                XPacketConverter
                .Serialize(XPacketType.GameRequest, new GameStartRequest(Login))
                .ToPacket());
        }

        public LoginViewModel(MainWindowViewModel mainWindow)
        {
            MainWindow = mainWindow;
        }
    }
}
