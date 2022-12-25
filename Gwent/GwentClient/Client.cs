using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Models.Dtos;
using Models.Dtos.GameStartRequest;
using Models.Dtos.GameStartResponse;
using Models.Dtos.MoveResult;
using Protocol;
using Protocol.Serializator;

namespace GwentClient;

public class Client
{
    private static int _handshakeMagic;

    private readonly Queue<byte[]> _packetSendingQueue = new();

    private Socket _socket;
    private IPEndPoint _serverEndPoint;
    public GameRunner GameRunner;
    public void Connect(string ip, int port)
    {
        Connect(new IPEndPoint(IPAddress.Parse(ip), port));
    }

    public void Connect(IPEndPoint server)
    {
        _serverEndPoint = server;
        var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        var ipAddress = ipHostInfo.AddressList[0];

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Connect(_serverEndPoint);

        Task.Run(ReceivePackets);
        Task.Run(SendPackets);
    }

    public void QueuePacketSend(byte[] packet)
    {
        if (packet.Length > 256)
        {
            throw new Exception("Max packet size is 256 bytes.");
        }

        _packetSendingQueue.Enqueue(packet);
    }

    private void ReceivePackets()
    {
        while (true)
        {
            var buff = new byte[256];
            _socket.Receive(buff);

            buff = buff.TakeWhile((b, i) =>
            {
                if (b != 0xFF) return true;
                return buff[i + 1] != 0;
            }).Concat(new byte[] { 0xFF, 0 }).ToArray();

            OnPacketRecieve(buff);
        }
    }
    
    private void OnPacketRecieve(byte[] packet)
    {
        var parsed = XPacket.Parse(packet);

        if (parsed != null)
        {
            ProcessIncomingPacket(parsed);
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
            case XPacketType.GameResponse:
                GameRunner.RunGame(XPacketConverter.Deserialize<AdaptedGameStartResponse>(packet).Parse());
                break;
            case XPacketType.MoveResult:
                GameRunner.ReceivingMovesQueue.Enqueue(XPacketConverter.Deserialize<AdaptedMoveResult>(packet).Parse());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private static void ProcessHandshake(XPacket packet)
    {
        var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

        if (_handshakeMagic - handshake.MagicHandshakeNumber == 15)
        {
            Console.WriteLine("Handshake successful!");
        }
    }

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
            _socket.Send(packet);

            Thread.Sleep(100);
        }
    }

    public void SendStartRequest(GameStartRequest request)
    {
        QueuePacketSend(XPacketConverter.Serialize(XPacketType.GameRequest, new AdaptedGameStartRequest(request)).ToPacket());
    }

    public void SendMove(PlayerMove move)
    {
        QueuePacketSend(XPacketConverter.Serialize(XPacketType.PlayerMove, move).ToPacket());
    }
}