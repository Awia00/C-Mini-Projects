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
        private Func<string, string> privateKey;
        private Func<string, string> publicKey; 

        public Form1()
        {
            InitializeComponent();
            textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName()); // get my IP
            foreach (var address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    textBox6.Text = address.ToString();
                }
            }
        }

        private void createKeys()
        {
            privateKey = new Func<string, string>(privateKeyMethod);
        }

        private string publicKeyMethod(string input)
        {
            string tempOutput = "";

            for (int i = 0; i < input.Length; i++)
            {
                tempOutput += (int)input.Substring(i, 1).ToCharArray()[0] + "-";
            }
            return tempOutput;
        }
        private string privateKeyMethod(string input)
        {
            string tempOutput = "";
            string[] inputChars = input.Split('-');
            foreach (var stringChar in inputChars)
            {
                if (!string.IsNullOrEmpty(stringChar))
                {
                    Console.WriteLine(stringChar);
                    tempOutput += (char)Int32.Parse(stringChar);
                }
            }
            Console.WriteLine(tempOutput);
            return tempOutput;
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
                        new MethodInvoker(delegate() { textBox2.AppendText("Encrypted: " + receive + " Decrypted: " + privateKeyMethod(receive) + "\n"); }));
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
                STW.WriteLine(publicKeyMethod(textToSend));
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
                textToSend = textBox1.Text.Trim();
                backgroundWorker2.RunWorkerAsync();
            }
            textBox1.Text = "";
        }

        private void CheckKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (textBox1.Text != "")
                {
                    textToSend = textBox1.Text.Trim();
                    backgroundWorker2.RunWorkerAsync();
                }
                textBox1.Text = "";
            }
        }
    }
}
