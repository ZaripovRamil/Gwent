using Protocol;
using System;
using System.Collections.Generic;
using TCPClient;

namespace GwentClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GameFieldViewModel GameField { get; }
        public LoginViewModel Login { get; }
        public XClient Client;
        public MainWindowViewModel()
        {
            //var client = new XClient();
            //Client = client;

            //client.OnPacketRecieve += OnPacketRecieve;
            //client.Connect("127.0.0.1", 4910);

            //Login = new LoginViewModel(this);

            GameField = new GameFieldViewModel(new List<int> { 1, 2, 3, 4, 5, 6, 1, 2 }, "Кексич", "PisiHunt");
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
                case XPacketType.Handshake:
                    
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}