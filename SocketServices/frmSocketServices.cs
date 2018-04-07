using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace SocketServices
{
    public partial class frmSocketServices : Form
    {
        public frmSocketServices()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Connection(txtIPAddress.Text.Trim(), Convert.ToInt32(txtProt.Text.Trim()));
        }
        static Socket ReceiveSocket;
        private void Connection(string ip, int port)
        {
            //IPAddress ipAddress = IPAddress.Parse(ip);
            IPAddress ipAddress = IPAddress.Any;
            ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ReceiveSocket.Bind(new IPEndPoint(ipAddress, port));
            ReceiveSocket.Listen(100);
            Thread thread = new Thread(watchConnecting);
            thread.IsBackground = true;
            thread.Start();
            SetTextValueInvokeRequired("Start Success");
        }
        void watchConnecting()
        {
            Socket connection = null;
            while (true)
            {
                try
                {
                    connection = ReceiveSocket.Accept();
                }
                catch (Exception e)
                {
                    SetTextValueInvokeRequired(e.Message);
                    break;
                }
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                connection.Send(Encoding.UTF8.GetBytes(string.Format("Address:{0}Port:{1} Connection Sucess", clientIP, clientPort.ToString())));
                SetTextValueInvokeRequired(connection.RemoteEndPoint.ToString() + " Connection");
                Thread thread = new Thread(new ParameterizedThreadStart(rec));
                thread.IsBackground = true;
                thread.Start(connection);
            }
        }
        void rec(object obj)
        {
            Socket socket = obj as Socket;
            while (true)
            {
                byte[] arrRecMsg = new byte[1024 * 1024];
                int length = socket.Receive(arrRecMsg);
                string strRecMsg = Encoding.UTF8.GetString(arrRecMsg, 0, length);
                SetTextValueInvokeRequired(string.Format("Client {0} Send Meassage [{1}]", socket.RemoteEndPoint, strRecMsg));
                socket.Send(Encoding.UTF8.GetBytes(string.Format("Hi [{0}]=>{1}", socket.RemoteEndPoint, Guid.NewGuid().ToString("N"))));
            }
        }

        private delegate void SetTextValueDelegate(string msg);
        private void SetTextValue(string msg)
        {
            txtConnecMessage.AppendText(string.Format("{0}   {1}{2}", DateTime.Now.ToString("yyyy-MM-dd:HH:mm:ss"), msg, Environment.NewLine));
        }
        private void SetTextValueInvokeRequired(string msg)
        {
            if (txtConnecMessage.InvokeRequired)
            {
                txtConnecMessage.Invoke(new SetTextValueDelegate(SetTextValue), new object[] { msg });
            }
            else { SetTextValue(msg); }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ReceiveSocket.Close();
        }
    }
}
