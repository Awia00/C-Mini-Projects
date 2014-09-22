using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerClientProject
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        public string receive;
        public string textToSend;

        public Form1()
        {
            InitializeComponent();

            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName()); // get my IP
            foreach (var address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    textBox6.Text = address.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Start Server
        {
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(textBox5.Text));
            listener.Start();
            client = listener.AcceptTcpClient();
            STR = new StreamReader(client.GetStream());
            STW = new StreamWriter(client.GetStream());
            STW.AutoFlush = true;

            backgroundWorker1.RunWorkerAsync(); // start recieving Data in background
            backgroundWorker2.WorkerSupportsCancellation = true; // Ability to cancel this thread
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) // recieve data
        {
            while (client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    this.textBox2.Invoke(
                        new MethodInvoker(delegate() { textBox2.AppendText("You : " + receive + "\n"); }));
                    receive = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e) // send data
        {
            if (client.Connected)
            {
                STW.WriteLine(textToSend);
                this.textBox2.Invoke(new MethodInvoker(delegate() { textBox2.AppendText("Me : " + textToSend + "\n"); }));
            }
            else
            {
                MessageBox.Show("Send Failed");
            }
            backgroundWorker2.CancelAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            IPEndPoint IP_End = new IPEndPoint(IPAddress.Parse(textBox3.Text),int.Parse(textBox4.Text));

            try
            {
                client.Connect(IP_End);
                if (client.Connected)
                {
                    textBox2.AppendText("Connected to Server" + "\n");
                    STW = new StreamWriter(client.GetStream());
                    STR = new StreamReader(client.GetStream());
                    STW.AutoFlush = true;

                    backgroundWorker1.RunWorkerAsync(); // start recieving Data in background
                    backgroundWorker2.WorkerSupportsCancellation = true; // Ability to cancel this thread
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e) // send cutton
        {
            if (textBox1.Text != "")
            {
                textToSend = textBox1.Text;
                backgroundWorker2.RunWorkerAsync();
            }
            textBox1.Text = "";
        }
    }
}
