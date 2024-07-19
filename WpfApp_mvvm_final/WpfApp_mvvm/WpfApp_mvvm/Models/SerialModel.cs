using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Collections.Concurrent;

namespace WpfApp_mvvm.Models
{
    internal class SerialModel : IDisposable
    {

        private SerialPort serialPort;
        public SerialModel(string portName, int baudRate) {
            serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += DataReceivedHandler;
        }
        public event Action<string> DataReceived;

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            DataReceived?.Invoke(data);
        }

        public void Open()
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
        }

        public void Close()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public void Write(string message)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(message);
            }
        }

        public void Dispose()
        {
            Close();
            serialPort?.Dispose();
        }
    }
}
