using Models;
using Models.Dtos;
using Protocol;
using Protocol.Serializator;

namespace TCPClient
{
    internal class Program
    {
        //TODO Move this to another Client

        private static void Main()
        {
            var game = new Game("Ramil", " Yakov");
            Console.WriteLine(game.IsRoundFinished);
            /*var move = XPacketConverter.Deserialize<PlayerMove>(
                XPacketConverter.Serialize(XPacketType.PlayerMove, new PlayerMove(0,false,1,1,1)));
            return;*/
            /*var client = new XClient();
            client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);

            var rand = new Random();
            _handshakeMagic = rand.Next();

            Thread.Sleep(1000);

            Console.WriteLine("Sending handshake packet..");

            client.QueuePacketSend(
                XPacketConverter.Serialize(
                        XPacketType.Handshake,
                        new XPacketHandshake
                        {
                            MagicHandshakeNumber = _handshakeMagic
                        })
                    .ToPacket());

            while (true)
            {
            }*/
        }

        
    }
}