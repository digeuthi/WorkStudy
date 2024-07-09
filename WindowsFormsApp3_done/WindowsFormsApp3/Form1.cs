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

        [DllImport("kernel32.dll")]
        private static extern bool ReleaseConsole();

        private MessageStack messageStack;
        private SerialPort serialPort;
        private bool isRunning = false;
        public Form1()
        {
            InitializeComponent();
            messageStack = new MessageStack();
            serialPort = new SerialPort();
            serialPort.PortName = "COM3"; // 포트 이름 설정
            serialPort.BaudRate = 115200;   // 통신 속도 설정
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            //Delegate인 DataReceived 동기적인 방식으로 진행된다.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                serialPort.Open(); // 시리얼 포트 열기
                AllocConsole(); // 콘솔 창 열기
                Console.WriteLine("Serial port opened");
                Task.Run(() => ProcessMessages()); // 비동기로 메시지 처리 시작
                //Task 비동기 작업 수행, 별도의 스레드에서 진행
                //스레드 풀에 지정된 작업을 실행
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var data = serialPort.ReadExisting();
                Console.WriteLine($"Data received : {data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading data: {ex.Message}");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                serialPort.Close(); // 시리얼 포트 닫기
                Console.WriteLine("Serial port closed");
                //ReleaseConsole();
            }
        }

        private void ProcessMessages()
        {

            Console.WriteLine("메시지를 입력하세요. (Enter 입력 시 종료_콘)");

            while (isRunning)
            {
                var message = Console.ReadLine();
                serialPort.Write(message);

                if (string.IsNullOrEmpty(message))
                    break;

                messageStack.PushMessage(message);
            }

            Console.WriteLine("메시지 처리 결과:");
            while (messageStack.Count > 0)
            {
                var popedMessage = messageStack.PopMessage();
                Console.WriteLine(popedMessage);
                serialPort.WriteLine(popedMessage);
            }
        }
    }
}
