using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    public class SocketClientServices
    {
        private Socket socket { get; set; }
        public SocketClientServices(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        }
        public string Sender(string message)
        {
            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            socket.Send(byteMessage);
            byte[] receive = new byte[1024];
            int length = socket.Receive(receive);
            return Encoding.UTF8.GetString(receive, 0, length);
        }
        public void Stop()
        {
            socket.Close();
        }
    }
}
