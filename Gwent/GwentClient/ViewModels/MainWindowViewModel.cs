using Protocol;
using ReactiveUI;
using System;
using TCPClient;

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

        public XClient Client;

        public MainWindowViewModel()
        {
            var client = new XClient();
            
            client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);
            Client = client;

            Content = new LoginViewModel(this);
        }

        private static void OnPacketRecieve(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private static void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.GameResponse:
                    
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}