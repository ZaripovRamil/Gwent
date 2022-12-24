using System.Net.Sockets;
using Models.Dtos;
using Models.Dtos.GameStartRequest;
using Models.Dtos.GameStartResponse;
using Models.Dtos.MoveResult;
using Protocol;
using Protocol.Serializator;

namespace Server;

public class ConnectedClient
{
    public Socket Client { get; }

    private readonly Queue<byte[]> _packetSendingQueue = new();
    private GameRunner? GameRunner { get; set; }
    public Server Server { get; }

    public ConnectedClient(Socket client, Server server)
    {
        Client = client;
        Server = server;
        Task.Run(ProcessIncomingPackets);
        Task.Run(SendPackets);
    }

    private void ProcessIncomingPackets()
    {
        while (true) // Слушаем пакеты, пока клиент не отключится.
        {
            var buff = new byte[1024]; // Максимальный размер пакета - 256 байт.
            Client.Receive(buff);

            buff = buff.TakeWhile((b, i) =>
            {
                if (b != 0xFF) return true;
                return buff[i + 1] != 0;
            }).Concat(new byte[] {0xFF, 0}).ToArray();

            var parsed = XPacket.Parse(buff);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }
    }

    private void ProcessIncomingPacket(XPacket packet)
    {
        var type = XPacketTypeManager.GetTypeFromPacket(packet);

        switch (type)
        {
            case XPacketType.Handshake:
                ProcessHandshake(packet);
                break;
            case XPacketType.Unknown:
                break;
            case XPacketType.GameRequest:
                var name = XPacketConverter.Deserialize<AdaptedGameStartRequest>(packet).Parse().PlayerName;
                GameRunner = Server.AddClientIntoGame(this, name);
                break;
            case XPacketType.PlayerMove:
                if (GameRunner is null) throw new NullReferenceException("Client did not join game yet");
                GameRunner.MovesQueue.Enqueue(XPacketConverter.Deserialize<PlayerMove>(packet));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ProcessHandshake(XPacket packet)
    {
        Console.WriteLine("Recieved handshake packet.");
        var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);
        handshake.MagicHandshakeNumber -= 15;
        Console.WriteLine("Answering..");
        QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
    }

    public void QueuePacketSend(byte[] packet)
    {
        _packetSendingQueue.Enqueue(packet);
    }

    public void QueuePacketSend(XPacketType type, object obj) =>
        QueuePacketSend(XPacketConverter.Serialize(type, obj).ToPacket());

    private void SendPackets()
    {
        while (true)
        {
            if (_packetSendingQueue.Count == 0)
            {
                Thread.Sleep(100);
                continue;
            }

            var packet = _packetSendingQueue.Dequeue();
            Client.Send(packet);
            Thread.Sleep(100);
        }
    }

    public void SendStartResponse(GameStartResponse startResponse)
    {
        QueuePacketSend(XPacketType.GameResponse, new AdaptedGameStartResponse(startResponse));
    }

    public void SendMoveResult(MoveResult moveResult)
    {
        QueuePacketSend(XPacketType.GameResponse, new AdaptedMoveResult(moveResult));
    }
}