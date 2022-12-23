namespace Server
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "GwentServer";
            Console.ForegroundColor = ConsoleColor.White;
            var server = new Server();
            server.Start();
            server.AcceptClients();
        }
    }
}