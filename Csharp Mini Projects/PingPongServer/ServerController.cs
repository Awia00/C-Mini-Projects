using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PingPongClient;
using PingPongCommon;

namespace PingPongServer
{
    class ServerConnection : UdpBase
    {
        private readonly IPEndPoint _listenOn;
        private readonly GameController _gameController;
        private Dictionary<string, Action> _commands; 

        public ServerConnection() : this(new IPEndPoint(IPAddress.Any,32123))
        {
        }

        public ServerConnection(IPEndPoint endpoint)
        {
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
            _gameController = new GameController();
            _commands = new Dictionary<string, Action>
            {
                { "play", () => _gameController.StartGame() }, 
                { "pause", () => _gameController.PauseGame() },
                { "restart", () => _gameController.RestartGame() }
            };
        }

        public void Reply(string message,IPEndPoint endpoint)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length,endpoint);
        }

        private void StartServer()
        {
            Console.WriteLine(@"Starting Server");
            //create a new server
            //start listening for messages and copy the messages back to the client
            var factory = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var received = await Receive();
                    Console.WriteLine(@"Message received: " + received.Message);
                    Reply("copy " + received.Message, received.Sender);
                    try
                    {
                        _commands[received.Message].Invoke();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            });
            Console.WriteLine(@"Listening...");

            string read;
            do
            {
                read = Console.ReadLine();
            } while (read != "quit");
            Console.WriteLine(@"Shutting down...");
            factory.Dispose();
        }

        static void Main(string[] args)
        {
            new ServerConnection().StartServer();
        }
    }
}
