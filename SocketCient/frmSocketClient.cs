using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocketClient
{
    public partial class frmSocketClient : Form
    {
        public frmSocketClient()
        {
            InitializeComponent();
            btnStop.Enabled = false;
            btnSend.Enabled = false;
        }
        private SocketClientServices socketClient;
        private void btnConnection_Click(object sender, EventArgs e)
        {
            socketClient = new SocketClientServices(txtIPAddress.Text.Trim(), Convert.ToInt32(txtProt.Text.Trim()));
            SetTextValue(string.Format("Address:{0} Connection {1} Success", txtIPAddress.Text.Trim(), txtProt.Text.Trim()));
            btnConnection.Text = "已启动连接 SUCCESS";
            btnConnection.Enabled = false;
            btnStop.Enabled = true;
            btnSend.Enabled = true;
        }
        private void SetTextValue(string value)
        {
            if (txtMessageContent.InvokeRequired)
            {
                txtMessageContent.Invoke(new InvokeCallback(SetValue), new object[] { value });
            }
            else
            {
                SetValue(value);
            }
        }
        private delegate void InvokeCallback(string msg);
        void SetValue(string msg)
        {
            txtMessageContent.AppendText(string.Format("{0}   {1}{2}", DateTime.Now.ToString("yyyy-MM-dd:HH:mm:ss"), msg, Environment.NewLine));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SetTextValue(socketClient.Sender(txtMsg.Text));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            socketClient.Stop();
            btnConnection.Enabled = true;
        }
    }
}
