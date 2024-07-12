using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Windows.Interop;
using System.Linq.Expressions;

namespace WpfApp
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // TODO : DataReceived 해결

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private SerialPort serialPort;
        private string message = string.Empty;

        private string[] messageArr = { "test1", "test2", "test3", "test4", "test5" };

        private BlockingCollection<string> messageQueue = new BlockingCollection<string>();
        private BlockingCollection<string> receiveQueue = new BlockingCollection<string>();

        //private StringBuilder receivedDataBuffer = new StringBuilder();
        //private bool receivingMessage = false;

        public MainWindow()
        {
            InitializeComponent();
            serialPort = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            Task.Run(() => SendMessages());
            Task.Run(() => ReceiveMessages());
            
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                string data = serialPort.ReadLine();
                receiveQueue.Add(data);

                /*foreach (char c in data) {
                    if (c == '$') {
                        receivingMessage = true;
                        receivedDataBuffer.Clear();
                    }

                    if (receivingMessage) { 
                        receivedDataBuffer.Append(c);
                    }

                    if (receivedDataBuffer.ToString().EndsWith("\r\n")) {
                        receivingMessage = false;
                        string completeMessage = receivedDataBuffer.ToString();
                        receiveQueue.Add(completeMessage);
                        receivedDataBuffer.Clear();
                    }
                }*/
                
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    Console.WriteLine($"Error in DataReceivedHandler: {ex.Message}");
                });
            }

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                serialPort.Open();
                AllocConsole();
                StartButton.IsEnabled = false;
                StopButton.IsEnabled = true;
                SetButtonVisible(Visibility.Visible);
                Console.WriteLine($"{DateTime.Now}: Serial port Open");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

            serialPort.Close();
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            SetButtonVisible(Visibility.Hidden);
            Console.WriteLine($"{DateTime.Now}: Serial port Close");
        }

        private void SetButtonVisible(Visibility visibility)
        {
            Button1.Visibility = visibility;
            Button2.Visibility = visibility;
            Button3.Visibility = visibility;
            InputTextBox.Visibility = visibility;
            Button4.Visibility = visibility;
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            messageQueue.Add("RandomNumber");

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            messageQueue.Add("RandomMessage");

        }


        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            messageQueue.Add("MessageFromFile");

        }


        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            messageQueue.Add(message);


        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            message = InputTextBox.Text;
        }


        private void SendMessages()
        {
            foreach (var message in messageQueue.GetConsumingEnumerable())
            {
                if (message == "RandomNumber")
                {

                    Dispatcher.Invoke(() =>
                    {
                        Console.WriteLine($"{DateTime.Now}: {message} sent ");
                    });
                    SendRandomNumber();
                }
                else if (message == "RandomMessage")
                {
                    Dispatcher.Invoke(() =>
                    {
                        Console.WriteLine($"{DateTime.Now}: {message} sent ");
                    });
                    SendRandomMessage();
                }
                else if (message == "MessageFromFile")
                {
                    Dispatcher.Invoke(() =>
                    {
                        Console.WriteLine($"{DateTime.Now}: {message} sent ");
                    });
                    SendMessageFromFile();
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        Console.WriteLine($"{DateTime.Now}: {message} sent ");
                    });
                    SendMessageFromTextBox(message);
                }

            }
        }

        private void ReceiveMessages()
        {

            try {
                foreach (var message in receiveQueue.GetConsumingEnumerable()) // 반복문이 끝나지 않는다.
                {
                    Dispatcher.Invoke(() =>
                    {
                        Console.WriteLine($"{DateTime.Now}: {message} received ");
                    });

                    if (message.Contains("$"))
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Console.WriteLine("Special command handling executed.");
                        });
                    }
                }

                Console.WriteLine("ReceiveMessages Out"); //여기까지 신호가 오지않음
            } catch(InvalidOperationException) { Console.WriteLine("ReceiveMessages completed."); }
        } 

        private void SendRandomNumber()
        {
            Random random = new Random();
            int nums = random.Next(0, 100);
            string message = $"${nums}\r\n";
            serialPort.Write(message);
            //Console.WriteLine("RandomNumber : ");

        }

        private void SendRandomMessage()
        {
            Random random = new Random();
            string message = $"${messageArr[random.Next(messageArr.Length)]}\r\n";
            serialPort.Write(message);
            //Console.WriteLine("RandomMessage : ");
        }

        private void SendMessageFromFile()
        {

            try
            {
                string message = File.ReadAllText("message.txt");
                message = $"${message}\r\n";
                serialPort.Write(message);
                //Console.WriteLine("MessageFromFile : ");

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

        }

        private void SendMessageFromTextBox(string message)
        {
            message = $"${message}\r\n";
            serialPort.Write(message);
            //Console.WriteLine("MessageTextFile : ");
        }


    }
}