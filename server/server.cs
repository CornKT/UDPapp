using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace server
{
    public partial class server : Form
    {
        Thread thdUDPServer;
        public server()
        {
            thdUDPServer = new Thread(new ThreadStart(serverThread));
            InitializeComponent();
        }
        public void serverThread()
        {
            int port = Convert.ToInt32(tbPort.Text);
            UdpClient udpClient = new UdpClient(port);
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                if (receiveBytes.Length == 0)
                    break;
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                string mess = RemoteIpEndPoint.Address.ToString() + ": " +
                returnData.ToString();
                WriteToTextBox(mess);
            }
            udpClient.Close();
        }

        private void WriteToTextBox(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<string>(WriteToTextBox), text);
            }
            else
            {
                richTextBox1.AppendText(text + '\n');
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (thdUDPServer.IsBackground)
            {
                thdUDPServer.Abort();
                return;
            }

            if (tbPort.Text.Length != 0 && !thdUDPServer.IsBackground)
            {
                thdUDPServer.Start();
                return;
            }
                
        }
    }
}
