using System.Text;
using PingPongCommon;

namespace PingPongClient.Connections
{
    public class ClientConnection : UdpBase
    {
        private ClientConnection() { }

        public static ClientConnection ConnectTo(string hostname, int port)
        {
            var connection = new ClientConnection();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }
}
