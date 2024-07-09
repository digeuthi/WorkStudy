﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using RandomNumber;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        private RandomNum randomNum;
        private SerialPort serialPort;


        public Form1()
        {
            InitializeComponent();
            randomNum = new RandomNum();
            serialPort = new SerialPort();
            serialPort.PortName = "COM3";
            serialPort.BaudRate = 115200;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        // TODO: Random 클래스 호출

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var data = serialPort.ReadExisting();
                Console.WriteLine($"RandomNumber : {data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading data: {ex.Message}");
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            serialPort.Open();
            AllocConsole();
            Console.WriteLine("Start");

            Task.Run(() => Process());
        }

        private void Process()
        {
            Console.WriteLine("Random Number");

            while (serialPort.IsOpen)
            {
                string nums = randomNum.GenerateRandomNum(10);
                serialPort.Write(nums);
                Console.WriteLine($"Sent: {nums}");

                Task.Delay(2000).Wait();
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                Console.WriteLine("Stop");
            }
        }

        private void Temp()
        {
            string s1 = "";

            string s2 = s1 + "12";
        }
    }
}