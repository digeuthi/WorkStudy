using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagerLibrary;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private MessageStack messageStack;
        private SerialPort serialPort;
        private bool isRunning = false;
        public Form1()
        {
            InitializeComponent();
            messageStack = new MessageStack();
            serialPort = new SerialPort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort.PortName = "COM3"; // 포트 이름 설정
            serialPort.BaudRate = 9600;   // 통신 속도 설정
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                serialPort.Open(); // 시리얼 포트 열기
                AllocConsole(); // 콘솔 창 열기
                Task.Run(() => ProcessMessages()); // 비동기로 메시지 처리 시작
                Console.WriteLine("Start");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                serialPort.Close(); // 시리얼 포트 닫기
                Console.WriteLine("Stop");
            }
        }

        private void ProcessMessages()
        {
            while (isRunning)
            {
                Console.WriteLine("메시지를 입력하세요. (Enter 입력 시 종료)");
                string message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                    break;
                messageStack.PushMessage(message);
            }

            Console.WriteLine("메시지 처리 결과:");
            while (messageStack.Count > 0)
            {
                Console.WriteLine(messageStack.PopMessage());
            }
        }
    }
}
