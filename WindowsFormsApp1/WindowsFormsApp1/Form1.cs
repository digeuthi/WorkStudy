using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Configuration;
using System.IO.Ports; 

namespace WindowsFormsApp1
{
    public partial class Form : System.Windows.Forms.Form
    {
        private SerialPort serialPort1;
        public Form()
        {
            InitializeComponent();
            serialPort1 = new SerialPort();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            serialPort1.PortName = comboBox_port.Text;
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Parity = Parity.None;

            serialPort1.DataReceived += SerialPort1_DataReceived;

            serialPort1.Open();  //시리얼포트 열기
            comboBox_port.Enabled = false;

        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.WriteLine("call");
            if (sender is SerialPort serialPort)
            {
                var receivedDataString = serialPort.ReadExisting();
                Debug.WriteLine($"{receivedDataString}");

                Invoke((Delegate)new Action(() =>
                {
                    var originalText = richTextBox1.Text;
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(originalText);
                    stringBuilder.Append("\r\n");
                    stringBuilder.Append(receivedDataString);

                    richTextBox1.Text = stringBuilder.ToString();
                }));
                
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox_port.DataSource = SerialPort.GetPortNames();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox_send.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            serialPort1.Close();
            comboBox_port.Enabled = true;
        }

        private void comboBox_port_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox_send_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
