using System;

namespace TCPServer
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "GwentServer";
            Console.ForegroundColor = ConsoleColor.White;

            var server = new Server.Server();
            server.Start();
            server.AcceptClients();
        }
    }
}