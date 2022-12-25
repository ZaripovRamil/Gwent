using ReactiveUI;

namespace GwentClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public Client Client;

        public MainWindowViewModel()
        {
            var client = new Client();
            client.Connect("127.0.0.1", 4910);

            Content = new LoginViewModel(this);
        }
    }
}