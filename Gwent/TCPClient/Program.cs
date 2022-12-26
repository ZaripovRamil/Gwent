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
            foreach (var card in Adapter.ParseCards(Adapter.Adapt(new byte[] {1, 2, 3, 4, 5, 1, 2, 3})))
            {
                Console.Write(card+" ");
            } ;
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