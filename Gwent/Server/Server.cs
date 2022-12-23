using System.Net;
using System.Net.Sockets;
using Models.Dtos;

namespace Server;

public class Server
{
    private readonly Socket _socket;
    private readonly List<GameRunner> _games;
    private Dictionary<ConnectedClient, GameRunner> ClientGameDictionary { get; set; }
    private Dictionary<string, ConnectedClient> NameClientDictionary { get; set; }

    private bool _listening;
    private bool _stopListening;

    public Server()
    {
        var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
        var ipAddress = ipHostInfo.AddressList[0];
        _games = new List<GameRunner>();
        _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        ClientGameDictionary = new Dictionary<ConnectedClient, GameRunner>();
        NameClientDictionary = new Dictionary<string, ConnectedClient>();
    }

    public void Start()
    {
        if (_listening)
        {
            throw new Exception("Server is already listening incoming requests.");
        }

        _socket.Bind(new IPEndPoint(IPAddress.Any, 4910));
        _socket.Listen(10);
        _listening = true;
    }

    public void Stop()
    {
        if (!_listening)
        {
            throw new Exception("Server is already not listening incoming requests.");
        }

        _stopListening = true;
        _socket.Shutdown(SocketShutdown.Both);
        _listening = false;
    }

    public void AcceptClients()
    {
        while (true)
        {
            if (_stopListening)
            {
                return;
            }

            Socket client;

            try
            {
                client = _socket.Accept();
            } catch { return; }

            Console.WriteLine($"[!] Accepted client from {(IPEndPoint) client.RemoteEndPoint!}");

            var c = new ConnectedClient(client, this);
        }
    }

    public GameRunner AddClientIntoGame(ConnectedClient client,string name)
    {
        NameClientDictionary[name] = client;
        LastGame.AddClient(name);
        ClientGameDictionary[client] = LastGame;
        return LastGame;
    }

    public GameRunner LastGame
    {
        get
        {
            if(_games.Count == 0||_games[^1].IsFilled)
                _games.Add(new GameRunner(this));
            return _games[^1];
        }
    }

    public void SendStartResponse(string playerName, GameStartResponse startResponse)
    {
        var client = NameClientDictionary[playerName];
        client.SendStartResponse(startResponse);
    }

    public void SendMoveResult(string playerName, MoveResult moveResult)
    {
        var client = NameClientDictionary[playerName];
        client.SendMoveResult(moveResult);
    }
}