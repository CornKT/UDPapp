using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace lab03
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }
        public void send()
        {
            //Tạo kết nối UDP
            UdpClient udpClient = new UdpClient();
            //Do ý đồ muốn gởi dữ liệu sang bên nhận. Nên cần chuyển chuỗi World sang kiểu byte
            Byte[] sendBytes = Encoding.ASCII.GetBytes(tbMessage.Text);
            //Gởi dữ liệu mà không cần thiết lập kết nối với Server
            udpClient.Send(sendBytes, sendBytes.Length, tbIP.Text, Convert.ToInt32(tbPort.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thdUDPClient = new Thread(new ThreadStart(send));
            thdUDPClient.Start();
        }
    }
}
