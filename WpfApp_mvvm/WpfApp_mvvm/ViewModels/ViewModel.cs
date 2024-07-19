using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using WpfApp_mvvm.Models;
using System.Collections;
using System.IO;
using System.Windows.Controls;

namespace WpfApp_mvvm.ViewModels
{
    class ViewModel : INotifyPropertyChanged //INotifyCollectionChanged
    {
  
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private SerialModel serialModel;
        private readonly string[] messageArr = { "test1", "test2", "test3", "test4", "test5" };
        //private readonly BlockingCollection<string> messageQueue = new BlockingCollection<string>();
        //private readonly BlockingCollection<string> receiveQueue = new BlockingCollection<string>();
        private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> receiveQueue = new ConcurrentQueue<string>();


        public ViewModel()
        {

            serialModel = new SerialModel("COM4", 115200);
            serialModel.DataReceived += DataReceivedHandler;

            Task.Run(() => SendMessages());
            Task.Run(() => ReceiveMessages());

            StartCommand = new RelayCommand(Start, () => !IsPortOpen);
            StopCommand = new RelayCommand(Stop, () => IsPortOpen);
            SendRandomNumberCommand = new RelayCommand(() => messageQueue.Enqueue("RandomNumber"), () => ButtonIsEnabled);
            SendRandomMessageCommand = new RelayCommand(() => messageQueue.Enqueue("RandomMessage"), () => ButtonIsEnabled);
            SendMessageFromFileCommand = new RelayCommand(() => messageQueue.Enqueue("MessageFromFile"), () => ButtonIsEnabled);
            SendMessageCommand = new RelayCommand(() => messageQueue.Enqueue(InputMessage), () => ButtonIsEnabled);
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand SendRandomNumberCommand { get; }
        public ICommand SendRandomMessageCommand { get; }
        public ICommand SendMessageFromFileCommand { get; }
        public ICommand SendMessageCommand { get; }

        private bool isPortOpen;
        public bool IsPortOpen
        {
            get => isPortOpen;
            set
            {
                isPortOpen = value;
                OnPropertyChanged(nameof(IsPortOpen));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private string inputMessage;
        public string InputMessage
        {
            get => inputMessage;
            set
            {
                inputMessage = value;
                OnPropertyChanged(nameof(InputMessage));
            }
        }

        private bool buttonIsEnabled = false;

        public bool ButtonIsEnabled {
        get => buttonIsEnabled;
            set 
            {
                buttonIsEnabled = value;
                OnPropertyChanged(nameof(ButtonIsEnabled));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private void DataReceivedHandler(string data)
        {
            receiveQueue.Enqueue(data);
        }

        private void Start()
        {
            try
            {
                serialModel.Open();
                AllocConsole();
                IsPortOpen = true;
                ButtonIsEnabled = true;
                OnPropertyChanged(nameof(ButtonIsEnabled));
                Console.WriteLine($"{DateTime.Now}: Serial port Open");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        private void Stop()
        {
            serialModel.Close();
            IsPortOpen = false;
            ButtonIsEnabled = false;
            OnPropertyChanged(nameof(ButtonIsEnabled));
            Console.WriteLine($"{DateTime.Now}: Serial port Close");
        }

        private void SendMessages()
        {
            
            while (true)
            {
                try
                {
                    if (messageQueue.TryDequeue(out var message))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Console.WriteLine($"{DateTime.Now}: {message} sent ");
                        });

                        switch (message)
                        {
                            case "RandomNumber":
                                SendRandomNumber();
                                break;
                            case "RandomMessage":
                                SendRandomMessage();
                                break;
                            case "MessageFromFile":
                                SendMessageFromFile();
                                break;
                            default:
                                SendMessageFromTextBox(message);
                                break;
                        }
                    }
                    else
                    {
                        Task.Delay(100).Wait(); // 메시지가 없을 때 잠시 대기
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in SendMessages: {ex.Message}");
                }
            }
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                try
                {
                    if (receiveQueue.TryDequeue(out var message))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Console.WriteLine($"{DateTime.Now}: {message} received ");
                        });

                        if (message.Contains("$"))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                Console.WriteLine("Special command handling executed.");
                            });
                        }
                    }
                    else
                    {
                        Task.Delay(100).Wait(); // 메시지가 없을 때 잠시 대기
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Error in ReceiveMessages: {ex.Message}");
                }

            }
        }

        private void SendRandomNumber()
        {
            Random random = new Random();
            int nums = random.Next(0, 100);
            string message = $"${nums}\r\n";
            serialModel.Write(message);
        }

        private void SendRandomMessage()
        {
            Random random = new Random();
            string message = $"${messageArr[random.Next(messageArr.Length)]}\r\n";
            serialModel.Write(message);
        }

        private void SendMessageFromFile()
        {
            try
            {
                string message = File.ReadAllText("message.txt");
                message = $"${message}\r\n";
                serialModel.Write(message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        private void SendMessageFromTextBox(string message)
        {
            message = $"${message}\r\n";
            serialModel.Write(message);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute();

        public void Execute(object parameter) => execute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
